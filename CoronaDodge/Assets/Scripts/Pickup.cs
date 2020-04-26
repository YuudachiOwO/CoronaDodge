using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] float infectionDecreaser = 0.04f;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Test_Player"))
        {   
            Healtbar.Instance.DecreaseInfection(infectionDecreaser);
            SoundManager.Instance.PlayAudioClip(SoundManager.SOUNDCLIP.Pickup);
            Destroy(transform.gameObject);
        }
    }
}
