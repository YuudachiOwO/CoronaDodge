﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healtbar : MonoBehaviour
{
    //public Playerhealth pHealth;
    public Image fill;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
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
}
