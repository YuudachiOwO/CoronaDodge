using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsserTOOLres {
	public class LevelManager : MonoBehaviour {

		[SerializeField] Transform target;
		[SerializeField] GameObject startLevel;
		[SerializeField] int queueLength = 2;
		[Range(0.0f, 1.0f)]
		[SerializeField] float threashHold = 0.5f;

		LevelHandle currentLevel;

		Queue<GameObject> loadedLevels = new Queue<GameObject>();

		void Start() {
#if UNITY_EDITOR
			if(!target) {
				Debug.LogError("FATAL: no taget to create levels for.");
				Destroy(this);
				return;
			}
			if(!startLevel) {
				Debug.LogError("FATAL: no start level to start travercing");
				Destroy(this);
				return;
			}
#endif
			// get start level
			currentLevel = CheckNextLevel(startLevel);
#if UNITY_EDITOR
			if(!currentLevel) {
				Destroy(this);
				return;
			}
#endif
			// adds start level to queue
			loadedLevels.Enqueue(startLevel);
		}

		private void FixedUpdate() {
			// early-out: not importend if he hasn't crossed the threashhold of half the length
			if(target.position.z <= (currentLevel.transform.position.z + currentLevel.levelLength * threashHold)) {
				return;
			}

			// dequeue old levels from the past
			if(loadedLevels.Count > queueLength) {
				Destroy(loadedLevels.Dequeue());
			}

			// choose next level part
			int random = Random.Range(0, currentLevel.nextLevels.Length);
			GameObject tmp = Instantiate(currentLevel.nextLevels[random]);

			// position next level part at the end of the current level
			tmp.transform.position = new Vector3(0, 0, currentLevel.transform.position.z + currentLevel.levelLength);

			// changing currentlevel to next level
			currentLevel = CheckNextLevel(tmp);
			loadedLevels.Enqueue(tmp);
		}

		LevelHandle CheckNextLevel(GameObject target) {
#if UNITY_EDITOR
			if(!target) {
				Debug.LogError("no next level");
				return null;
			}
#endif
			// retreave LevelHandle
			LevelHandle value = target.GetComponent<LevelHandle>();
#if UNITY_EDITOR
			// checking values
			if(value == null) {
				Debug.LogError("next level has no LevelHandler");
				return null;
			}
			if(value.levelLength <= 0) {
				Debug.LogError("next level has no length");
				return null;
			}
			if(value.nextLevels == null) {
				Debug.LogError("next level has no next levels");
				return null;
			}
			// checking values one node further the line
			foreach(var it in value.nextLevels) {
				if(it == null) {
					Debug.LogError("an element in the next level list is missing");
					return null;
				}
				if(!it.GetComponent<LevelHandle>()) {
					Debug.LogError("the level " + it.name + " in next levels has no level handler");
					return null;
				}
			}
#endif
			return value;
		}
	}

}
