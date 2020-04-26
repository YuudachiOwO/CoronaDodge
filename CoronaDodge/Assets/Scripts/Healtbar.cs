using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healtbar : MonoBehaviour
{
	//public Playerhealth pHealth;
	public Image fill;
	private Slider slider;
	public GameObject menuContainer;

	[SerializeField] private float fillPercent = 0f;

	void Start()
	{
		slider = GetComponent<Slider>();

		fillPercent = 0;
	}

	void FixedUpdate()
	{
		slider.value = fillPercent;
		// this needs to be rewritten, condition to show UI is no longer a trigger

		if(slider.value >= 1)
		{
			menuContainer.SetActive(true);
		}
	}

	public void Infection(float f)
	{
		Mathf.Clamp01(fillPercent += f);
	}
}
