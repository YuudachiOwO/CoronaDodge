using UnityEngine;

public class CoronaColor : MonoBehaviour
{
	private Transform enemy;
	private CapsuleCollider coronaCollider;
	private float distance;

	[SerializeField] private Color baseColor = new Color(1, 1, 1, 1);
	[SerializeField] private Color coronaColor = new Color(.75f, .25f, 0, 1);
	private float percentage = 1f;
	// Start is called before the first frame update
	void Start()
	{
		coronaCollider = GetComponent<CapsuleCollider>();
		if (!coronaCollider)
			throw new MissingComponentException("Corona Color is missing a CapsuleCollider component");
		distance = coronaCollider.radius;
	}

	// Update is called once per frame
	void Update()
	{
		if (enemy != null)
		{
			distance = Vector3.Distance(transform.position, enemy.position);

			percentage = coronaCollider.radius / distance;
		}
		Color.Lerp(baseColor, coronaColor, percentage);
	}

	private void OnTriggerEnter(Collider other)
	{
		enemy = other.transform;
	}

	private void OnTriggerExit(Collider other)
	{
		enemy = null;
	}

}
