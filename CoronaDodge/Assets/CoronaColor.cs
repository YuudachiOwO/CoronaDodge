using UnityEngine;

public class CoronaColor : MonoBehaviour
{
	[SerializeField] private Transform enemy;
	private CapsuleCollider coronaCollider;
	private SpriteRenderer circle;
	private float distance;
	[SerializeField] private float minCollisionDist = 1.5f;

	[SerializeField] private Color baseColor = new Color(1, 1, 1, 1);
	[SerializeField] private Color coronaColor = new Color(.75f, .25f, 0, 1);
	private float percentage = 1f;
	// Start is called before the first frame update
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
	void Update()
	{
		if (enemy != null)
		{
			
			distance = Vector3.Distance(transform.position, enemy.position) - minCollisionDist;

			percentage = distance / coronaCollider.radius;
			print(percentage);
		}
		circle.color = Color.Lerp(coronaColor, baseColor, percentage);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			enemy = other.transform;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		enemy = null;
	}
}
