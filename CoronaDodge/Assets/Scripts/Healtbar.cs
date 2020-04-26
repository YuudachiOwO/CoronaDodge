using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healtbar : MonoBehaviour
{
    //public Playerhealth pHealth;
    public Image fill;
    private Slider slider;

	[SerializeField] private float fillPercent = 0f;

    void Start()
    {
        slider = GetComponent<Slider>();

		fillPercent = 0;
    }

    void FixedUpdate()
    {
		slider.value = fillPercent;

        /*if (slider.value <= slider.minvalue)
        {
            fill.enabled = false;
        }

        if (slider.value > slider.minvalue && !fill.enabled)
        {
            fill.enabled = true;
        }

        float fillvalue = pHealth.currenthealth / pHealth.maxhealth;

        slider.value = fillvalue;*/
    }

	public void Infection(float f)
	{
		Mathf.Clamp01(fillPercent += f);		
	}
}
