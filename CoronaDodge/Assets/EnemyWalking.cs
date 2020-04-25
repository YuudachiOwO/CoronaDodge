﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalking : MonoBehaviour
{
	// an array of directions to walk in
	[Tooltip("vectors fill be normalized")]
	public Vector2[] directions;
	// an array of seconds to walk
	[Tooltip("value in seconds to walk in the corresponding direction")]
	[SerializeField] private float[] durations;
	[Tooltip("the speed to walk in the corresponding direction - choose values between '0' and '2'")]
	[SerializeField] private float[] velocities;
	[SerializeField] private float velocityManipulator = 100f;
	private float currentTime;
	private float durationTime;
	private int current = 0;
	private float timer = 0;

	private Vector3 direction;

	void Start()
	{
		if (durations.Length != 0)
		{
			durationTime = Time.time + durations[current];
			//print($"Time.time: {Time.time} | duration time: {durationTime}");
		}
		if (current > directions.Length)
			Debug.LogError($"{current} is used to access the targets[] but is out of range!");
		timer = 0;
	}

	void Update()
	{
		print(current);

		currentTime = Time.time;
		if (currentTime < durationTime)
		{
			direction = new Vector3(directions[current].x, 0, directions[current].y);
			// outdated line:
			//transform.position += direction.normalized * (velocities[current] / velocityManipulator);

			var next = current < directions.Length - 1 ? current + 1 : 0;
			// 0 to 1 within durations[current] seconds
			timer += (1 / durations[current]) * Time.deltaTime; 
			var velocityLerp = Mathf.Lerp(velocities[current], velocities[next], timer);
			transform.position += direction.normalized * (velocityLerp / velocityManipulator);
		}
		else
		{
			if (current < directions.Length - 1)
			{
				current++;
			}
			else
			{
				current = 0;
			}

			timer = 0;
			durationTime = currentTime + durations[current];
		}
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
}
