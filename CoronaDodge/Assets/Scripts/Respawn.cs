using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    /* public Transform player;
    public Transform respawnPoint;

    private void OnTriggerEnter(Collider col)
    {
        player.transform.position = respawnPoint.transform.position; //When player enters the collider, reset position to the respawnpoint
    } */

    public void click()
    {
        SceneManager.LoadScene("SampleScene"); //LoadScene SampleScene when clicked on button
    }

    /* private void Start()
    {
        var testValue1 = 21;
        var testValue2 = 12;
        print("testValue1: " + testValue1 + " | testValue2: " + testValue2);
        print($"testValue1: {testValue1} | testValue2: {testValue2}");
    } */

}