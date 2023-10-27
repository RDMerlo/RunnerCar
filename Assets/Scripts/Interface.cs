using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{

    [Header("Поле вывода очков")]
    public Text textScore;

    [Header("Поле вывода скорости")]
    public Text textSpeed;

    [Header("Поле вывода таймера")]
    public Text textTimer;

    [Header("Поле вывода столкновений")]
    public Text textCrash;

    [Header("Меню")]
    public GameObject menu;

    [Header("Меню проигрыша")]
    public GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 velocity = (Settings.currentPosition - Settings.previousPosition) / Time.deltaTime;
        // float MySpeed = velocity.magnitude * 3.6f;

        int sec = 0, min = 0, hour = 0;

        sec = (int)Math.Round(Settings.timer, 0);

        if (sec >= 60){
            min = sec / 60;
            sec %= 60;
        }

        if (min >= 60){
            hour = min / 60;
            min %= 60;
        }

        textScore.text = "Заработано очков: " + Settings.score;
        textTimer.text = hour + ":" + min + ":" + sec;
        textSpeed.text = "Скорость: " + Math.Abs(Settings.speed * 360).ToString("0.0") + " км/ч";
        textCrash.text = "Столкновений: " + Settings.crash + "/10";

        if (Settings.gameOver){
            gameOver.SetActive(true);
            menu.SetActive(true);
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver.activeSelf){
            menu.SetActive(!menu.activeSelf);

            if (menu.activeSelf){
                Time.timeScale = 0;
            } else {
                Time.timeScale = 1;
            }
        }
    }
}
