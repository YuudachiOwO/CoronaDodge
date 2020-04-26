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
    [SerializeField] float[] timeValues = new float[2];
	[SerializeField] private float fillPercent = 0f;
    float timer = 0f;

    #region Singleton
    static Healtbar instance;
    public static Healtbar Instance
    {
        get
        {
            if (!instance)
            {
                Debug.LogError("[SCENECONTROLLER]: i'm the instance, i do not exist.");
            }

            return instance;
        }
    }
    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    void Start()
	{
		slider = GetComponent<Slider>();
		fillPercent = 0;
	}

	void FixedUpdate()
	{
		slider.value = fillPercent;
        timer -= Time.deltaTime;
        if(slider.value >= 50 && timer <= 0)
        {
            ResetTimer();
        }
        
        SoundManager.Instance.SetCoughingVolume(fillPercent);
		// this needs to be rewritten, condition to show UI is no longer a trigger

		if(slider.value >= 1)
		{
			menuContainer.SetActive(true);
		}
	}

    public void DecreaseInfection(float _value)
    {
        fillPercent -= _value;
    }

    private void ResetTimer()
    {
        if(timer <= 0)
        {
            timer = Random.Range(timeValues[0], timeValues[1]);
            SoundManager.Instance.PlayAudioClip(SoundManager.SOUNDCLIP.Coughing);
        }
    }

	public void Infection(float f)
	{
		Mathf.Clamp01(fillPercent += f);
	}
}
