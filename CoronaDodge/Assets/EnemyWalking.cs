using System.Collections;
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
	}

	void Update()
	{
		print(current);

		currentTime = Time.time;
		if (currentTime < durationTime)
		{
			direction = new Vector3(directions[current].x, 0, directions[current].y);
			transform.position += direction.normalized * (velocities[current] / velocityManipulator);
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
			durationTime = Time.time + durations[current];
		}
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
