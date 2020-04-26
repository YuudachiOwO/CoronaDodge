using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private GameObject player;
	[SerializeField] private Vector3 playerPos;

	// cmaera clamping boarders
	[SerializeField] private float offset = 12.3f;
	[SerializeField] private float xMin = 0;
	[SerializeField] private float xMax = 0;
	[SerializeField] private float zMin = 0;
	[SerializeField] private float zMax = 0;

	[SerializeField] private Vector3 cameraOffset;

	[Range(0.01f, 1.0f)]
	public float smoothSpeed = 0.25f;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		if (!player)
			throw new MissingComponentException("missing player reference");
		playerPos = player.transform.position;

		cameraOffset = GetComponentInChildren<Camera>().transform.position - playerPos;

		xMax = offset;
		xMin = -offset;
		zMax = playerPos.z + xMax;
		zMin = playerPos.z - xMax;
	}

	private void FixedUpdate()
	{
		playerPos = player.transform.position;
		// save the clamped borders in local variables
		var camClampX = Mathf.Clamp(playerPos.x, xMin, xMax);
		zMax = playerPos.z + xMax;
		if (playerPos.z > zMin + xMax)
		{
			zMin = playerPos.z - xMax;
		}
		var camClampZ = Mathf.Clamp(playerPos.z, zMin, zMax);

		// set the target vector inside the clamped values and slerp to it
		Vector3 targetPos = new Vector3(camClampX, playerPos.y, camClampZ);
		transform.position = Vector3.Slerp(transform.position, targetPos, smoothSpeed);
	}
}
