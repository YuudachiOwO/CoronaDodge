using UnityEngine;

public class CoronaColor : MonoBehaviour
{
	[SerializeField] Healtbar healtbar;
	//private Transform enemy;
	private CapsuleCollider coronaCollider;
	private SpriteRenderer circle;
	private float distance;

	[SerializeField] [Range(0.01f, 1f)] private float infectionStrength = 0.1f;
	[SerializeField] private float minCollisionDist = 1.35f;

	[SerializeField] private Color baseColor = new Color(1, 1, 1, 1);
	[SerializeField] private Color coronaColor = new Color(.75f, .25f, 0, 1);
	private float percentage = 1f;

	// new version
	private float shortestDist;
	[SerializeField] private Transform[] enemies;
	private int enemyCounter = 0;

	void Start()
	{
		coronaCollider = GetComponent<CapsuleCollider>();
		if (!coronaCollider)
			throw new MissingComponentException("Corona Color is missing a CapsuleCollider component");

		circle = GetComponent<SpriteRenderer>();
		if (!circle)
			throw new MissingComponentException("Corona Color is missing a SpriteRenderer component");
		circle.color = baseColor;

		distance = coronaCollider.radius;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (enemies.Length != 0)
		{
			shortestDist = coronaCollider.radius;
			foreach (var enemy in enemies)
			{
				float enemyDistance = (Vector3.Distance(enemy.position, transform.position));
				if (enemyDistance < shortestDist)
				{
					shortestDist = enemyDistance;
				}
			}
			distance = shortestDist;

			Mathf.Clamp01(percentage = 1 - (distance - minCollisionDist) / (coronaCollider.radius));
		}
		else
		{
			percentage = 0;
		}
		circle.color = Color.Lerp(baseColor, coronaColor, percentage);

		if (distance < coronaCollider.radius)
		{
			healtbar.Infection(percentage * infectionStrength);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			enemyCounter++;
            SoundManager.Instance.PlayAudioClip(SoundManager.SOUNDCLIP.Warning);
			var temp = enemies;
			enemies = new Transform[enemyCounter];
			for (int i = 0; i < temp.Length; i++)
			{
				enemies[i] = temp[i];
			}
			enemies[enemyCounter - 1] = other.transform;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Enemy")
		{
			enemyCounter--;

			var temp = new Transform[enemyCounter];
			if (temp.Length != 0)
			{
				var count = 0;
				for (int i = 0; i < enemies.Length; i++)
				{
					if (enemies[i] != other)
					{
						temp[count] = enemies[i];
						count++;
					}
				}
			}
			enemies = temp;
		}
	}
}
