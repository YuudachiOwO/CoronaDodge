using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] coughingVariations = new AudioClip[3];
    [SerializeField] AudioClip[] WarningVariations = new AudioClip[2];
    [SerializeField] AudioClip[] PickupVariations = new AudioClip[2];

    AudioSource[] WarningSound = new AudioSource[2];
    AudioSource[] CoughingSound = new AudioSource[2];
    AudioSource[] PickupSound = new AudioSource[2];
    AudioSource FootStepSound;

    #region Singleton
    static SoundManager instance;
    public static SoundManager Instance
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

    public enum SOUNDCLIP { Coughing, Warning, Footsteps, Pickup }

    // Start is called before the first frame update
    void Start()
    {
        InitializeArrays();
    }

    public void PlayAudioClip(SOUNDCLIP _SoundArea)
    {
        int range = 0;
        switch (_SoundArea)
        {
            case SOUNDCLIP.Coughing:
                range = Random.Range(0, coughingVariations.Length - 1);
                PlayAudioOnFirstFreeAvailable(CoughingSound, coughingVariations[range]);
                break;
            case SOUNDCLIP.Warning:
                range = Random.Range(0, WarningVariations.Length - 1);
                PlayAudioOnFirstFreeAvailable(WarningSound, WarningVariations[range]);
                break;
            case SOUNDCLIP.Footsteps:

                if (FootStepSound.isPlaying) FootStepSound.Stop();
                else { FootStepSound.Play(); }
                break;
            case SOUNDCLIP.Pickup:
                range = Random.Range(0, PickupVariations.Length - 1);
                PlayAudioOnFirstFreeAvailable(PickupSound, PickupVariations[range]);
                break;
            default:
                break;

        }
    }

    public void ToggleFootSteps(bool _isRunning)
    {
        if (!_isRunning && FootStepSound.isPlaying)
        {
            FootStepSound.Stop();
        }else if(_isRunning && !FootStepSound.isPlaying)
        {
            FootStepSound.Play();
        }
    }
    private void PlayAudioOnFirstFreeAvailable(AudioSource[] myQueue, AudioClip myClip)
    {
        if (!myQueue[0].isPlaying)
        {
            if (myClip != myQueue[0].clip) myQueue[0].clip = myClip;
            myQueue[0].Play();
        }
        //else if (myQueue[0].isPlaying && !myQueue[1].isPlaying)
        //{
        //    if (myClip != myQueue[1].clip) myQueue[1].clip = myClip;
        //    myQueue[1].Play();
        //}
        else
        {
            Debug.Log(myClip.name + " could not been played on this Object: " + this.gameObject.name);
        }
    }

    public void SetCoughingVolume(float _value)
    {
        
        CoughingSound[0].volume = _value >= 1? 1: _value;
    }

    private void InitializeArrays()
    {
        WarningSound = transform.Find("WarningSound").GetComponents<AudioSource>();
        CoughingSound = transform.Find("CoughingSound").GetComponents<AudioSource>();
        FootStepSound = transform.Find("FootstepSound").GetComponent<AudioSource>();
        PickupSound = transform.Find("PickupSound").GetComponents<AudioSource>();
    }
}
