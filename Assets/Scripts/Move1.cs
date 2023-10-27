using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move1 : MonoBehaviour
{

    [Header("Префаб района")]
    public GameObject district;

    [Header("Префаб монеты")]
    public GameObject money;

    [Header("Префаб препятствий")]
    public GameObject[] hamper;

    private System.Random rnd = new System.Random();

    private int position = 200; //блок карты
    

    private float rotX = 0, rotY = 0, rotZ = 0;

    public AudioClip sound;
    public AudioClip sound_2;
    public AudioClip soundMoney;
    AudioSource audio;


    void CreateObj(){
        int LR;
        
        for (int i = 0; i < 10; i++){
            if (rnd.Next(0,100) < 50) {
                LR = -1;
            } else {
                LR = 1;
            }
            Instantiate(money, new Vector3(LR * 1.55f, 1, position - 90 + i * 20 + rnd.Next(-5, 5)), Quaternion.identity);
            
            int rotYHamper = LR == -1 ? 0 : 180;

            int selectCar = rnd.Next(0,3);
            float LRCar = selectCar != 1 ? -LR * 1.50f : -LR * 1.40f;
            if (rnd.Next(0,100) < 50 && i % rnd.Next(1,3) == 0){
                Instantiate(hamper[selectCar], new Vector3(LRCar, 1f, position - 90 + i * 20 + rnd.Next(-5, 5)), Quaternion.Euler(new Vector3(0, rotYHamper, 0)));
            } else {
                Instantiate(hamper[selectCar], new Vector3(LRCar, 1f, position - 90 + i * 20 + rnd.Next(-5, 5)), Quaternion.Euler(new Vector3(0, rotYHamper, 0)));
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hamper"){
            Settings.speed = 0 * Time.deltaTime;
            Debug.Log("Скорость: " + Settings.speed);
            Settings.crash += 1;
            if (Settings.crash >= 10) Settings.gameOver = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Hamper"){
            Settings.speed = 0 * Time.deltaTime;
            Debug.Log("Скорость: " + Settings.speed);
            if (Settings.crash >= 10) Settings.gameOver = true;
        }
    }

    private void OnTriggerEnter(Collider temp) {
        if (temp.tag == "Respawn"){
            Instantiate(district, new Vector3(0, 0, position), Quaternion.identity);
            CreateObj();
            position += 200;
        }

        if (temp.tag == "Gold"){
            audio.PlayOneShot(soundMoney);
            Settings.score++;
            Destroy(temp.gameObject);
            Debug.Log("Собрано монет: " + Settings.score);
        }

        // if (temp.tag == "Hamper"){
        //     Settings.speed = 0 * Time.deltaTime;
        //     Debug.Log("Скорость: " + Settings.speed);
        // }
    }

    // Start is called before the first frame update
    void Start()
    {
        Settings.posPlayer = transform.position;
        Settings.speed = 0;
        Settings.score = 0;
        Settings.crash = 0;
        Settings.timer = 0;

        audio = GetComponent<AudioSource>();
        audio.volume = 0.18f;

        // Settings.currentPosition = transform.position;
        // Settings.gameOver = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Settings.previousPosition = Settings.currentPosition;
        // Settings.currentPosition = transform.position;

        Settings.timer += Time.deltaTime;
        Settings.posPlayer = transform.position;
        transform.position += transform.forward * Settings.speed; 

        if (Input.GetKey(KeyCode.UpArrow)){
            Settings.speed += 0.2f * Time.deltaTime;
            if (Settings.speed > 0.5f) Settings.speed = 0.5f;
            audio.PlayOneShot(sound);
        }

        if (Input.GetKey(KeyCode.DownArrow)){
            Settings.speed -= 0.5f * Time.deltaTime;
            if (Settings.speed < -0.1f) Settings.speed = -0.1f;
            audio.PlayOneShot(sound_2);
        }

        if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)){
            if (Settings.speed != 0){
                if (Settings.speed > 0) Settings.speed -= 0.05f * Time.deltaTime;
                if (Settings.speed < 0) Settings.speed += 0.05f * Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow)){
            rotY -= 1f * Math.Sign(Math.Round(Settings.speed, 2));
            // if (rotY < -45.0f) rotY = -45;
            transform.rotation = Quaternion.Euler(0, rotY, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow)){
            rotY += 1f * Math.Sign(Math.Round(Settings.speed, 2));
            // if (rotY > 45.0f)  rotY = 45;
            transform.rotation = Quaternion.Euler(0, rotY, 0);
        }        
    }
}
