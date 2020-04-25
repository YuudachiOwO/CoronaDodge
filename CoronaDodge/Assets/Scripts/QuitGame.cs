using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{

    public void quitGame()
    {
        Application.Quit(); //Quit Game button
        UnityEditor.EditorApplication.isPlaying = false; //Quit Game in Unity Editor
    }

}
