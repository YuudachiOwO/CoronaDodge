using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private Transform playerPos;

	// cmaera clamping boarders
	[SerializeField] private float xMin = -10f;
	[SerializeField] private float xMax = 10f;
	[SerializeField] private float zMin = -10f;
	[SerializeField] private float zMax = 10f;

	private Vector3 cameraOffset;

	[Range(0.01f, 1.0f)]
	public float smoothSpeed = 0.25f;

	private void Start()
	{
		if (!playerPos)
		{
			playerPos = transform;
			throw new MissingComponentException($"{transform.name} is missing a {typeof(Transform)} reference");
		}
		cameraOffset = transform.position - playerPos.position;
	}

	private void FixedUpdate()
	{
		// save the clamped borders in local variables
		var camClampX = Mathf.Clamp(playerPos.position.x, xMin, xMax);
		var camClampZ = Mathf.Clamp(playerPos.position.z, zMin, zMax);

		// set the target vector inside the clamped values and slerp to it
		Vector3 targetPos = new Vector3(camClampX, playerPos.position.y, camClampZ) + cameraOffset;
		transform.position = Vector3.Slerp(transform.position, targetPos, smoothSpeed);
	}
}
