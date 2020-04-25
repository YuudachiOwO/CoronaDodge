using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void click()
    {
        SceneManager.LoadScene("MainMenu"); //LoadScene when button is clicked
    }
    public void clickStart()
    {
        SceneManager.LoadScene("SampleScene"); //LoadScene when button is clicked
    }
    public void clickCredits()
    {
        SceneManager.LoadScene("Credits"); //LoadScene when button is clicked
    }

}
