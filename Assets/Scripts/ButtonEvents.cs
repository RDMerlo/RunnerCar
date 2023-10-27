using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvents : MonoBehaviour
{
    public void RestartButton(){
        Application.LoadLevel(0);
        Settings.gameOver = false;
        Time.timeScale = 1;
    }

    public void ExitButton(){
        Application.Quit();
    }
}
