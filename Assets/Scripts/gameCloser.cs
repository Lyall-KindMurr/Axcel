using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameCloser : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseGame();
    }

    public void CloseGame()
    {
        Debug.Log("I quit, im done, i'm outta here");
        Application.Quit();
    }
}
