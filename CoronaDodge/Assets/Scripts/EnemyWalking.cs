using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalking : MonoBehaviour
{
	[SerializeField] private bool lerpDirection = false;
	[SerializeField] private bool lerpVelocity = false;

	// an array of directions to walk in
	[Tooltip("vectors fill be normalized")]
	public Vector2[] directions = new Vector2[1];
	// an array of seconds to walk
	[Tooltip("value in seconds to walk in the corresponding direction")]
	[SerializeField] private float[] durations;
	[Tooltip("the speed to walk in the corresponding direction - choose values between '0' and '2'")]
	[SerializeField] private float[] velocities;
	[SerializeField] private float velocityManipulator = 100f;
	//private float currentTime;

	private Vector3 walkDirection;
	private float walkDuration = 0;
	private float walkVelocity = 0;

	private int current = 0;
	private int next = 1;
	private float timer = 0;
	Animator myAnimator;
	
	void Start()
	{
		myAnimator = transform.GetComponentInChildren<Animator>();

		if (durations.Length != 0)
		{
			walkDuration = Time.time + durations[current];
			myAnimator.SetFloat("Blend", CheckAnimationValue(new Vector3(directions[0].x, directions[0].y, 0)));
			//print($"Time.time: {Time.time} | duration time: {durationTime}");
		}
		if (current > directions.Length)
			Debug.LogError($"{current} is used to access the targets[] but is out of range!");
		timer = 0;
	}

	void Update()
	{
		Walking();
	}

	private float CheckAnimationValue(Vector3 _NextPosition)
	{
		Vector3 NullVector = new Vector3(0, 0, 0);
		float animationValue = _NextPosition.Equals(NullVector) ? 0 : 1;
		return animationValue;
	}

	private void OnValidate()
	{
		if (durations.Length != directions.Length)
		{
			durations = new float[directions.Length];
		}

		if (velocities.Length != directions.Length)
		{
			velocities = new float[directions.Length];
		}
	}

	private void Walking()
	{
		if (Time.time < walkDuration)
		{
			// timer to lerp from 0 to 1 within durations[current] seconds
			timer += (1 / durations[current]) * Time.deltaTime;

			next = current < directions.Length - 1 ? current + 1 : 0;

			if (lerpDirection)
			{
				var walkCurrent = new Vector3(directions[current].x, 0, directions[current].y).normalized;
				var walkNext = new Vector3(directions[next].x, 0, directions[next].y).normalized;
				walkDirection = Vector3.Lerp(walkCurrent, walkNext, timer);
			}
			else
			{
				walkDirection = new Vector3(directions[current].x, 0, directions[current].y).normalized;
			}
			if (lerpVelocity)
			{
				walkVelocity = (Mathf.Lerp(velocities[current], velocities[next], timer) / velocityManipulator);
			}
			else
			{
				walkVelocity = (velocities[current] / velocityManipulator);
			}

			transform.position += walkDirection * walkVelocity;
		}
		else
		{
			// reset the lerp timer
			timer = 0;

			if (current < directions.Length - 1)
			{
				current++;
			}
			else
			{
				current = 0;
			}

			walkDuration = Time.time + durations[current];

			myAnimator.SetFloat("Blend", CheckAnimationValue(walkDirection));
		}
	}
}
