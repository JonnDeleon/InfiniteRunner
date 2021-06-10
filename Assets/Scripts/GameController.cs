using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text nextLevelText;
    private float timeToAppear = 2f;
    private float timeWhenDisappear;

    public int maxHealth = 100;
    public int health = 50;
    public int speed = 10;
    public int jumpIntensity = 7;
    public int timer = 30;
    public int cheeseCount;
    public int level = 1;
    public int totalCheese = 0;

    public GameOverScreen GameOverScreen;

    [SerializeField] private Text levelText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text healthText;
    [SerializeField] private Text speedText;
    [SerializeField] private Text cheeseCountText;
    [SerializeField] private bool isPaused = false;
    [SerializeField] private GameObject healthBar;

    private void Start()
    {
        // Load information
        health = PlayerPrefs.GetInt("PlayerHealth", 50);
        speed = PlayerPrefs.GetInt("PlayerSpeed", 10);
        StartCoroutine("CountDown");
        cheeseCount = 0;
    }

    IEnumerator CountDown()
    {

        while (timer > 0)
        {
            timerText.text = "Timer = " + timer;
            healthText.text = "Health = " + health + "/" + maxHealth ;
            speedText.text = "Speed = " + speed;
            cheeseCountText.text = "Cheese Count = " + cheeseCount;
            levelText.text = "LEVEL " + level;
            // Wait for 1 second
            yield return new WaitForSeconds(1);
            if (!isPaused)
                timer--;
        }

        //GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = false;
        while (isPaused)
            yield return null;
        timer = 30;
        EnableText();
        ChangeLevel();
    }

    private void ChangeLevel()
    {
        level += 1;
        speed += 5;
        // EnableText();
        StartCoroutine("CountDown");
    }

    private void Update()
    {
        healthBar.transform.localScale = new Vector3(health / 100f, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        if (health <= 0)
            GOscreen();
        if (cheeseCount == 10)
        {
            cheeseCount = 0;
            if (health != 100)
            {
                health = 100;
            }
            else
            {
                maxHealth += 10;
            }
        }
        //NEXT LEVEL TEXT REMOVAL
        if (nextLevelText.enabled && (Time.time >= timeWhenDisappear))
        {
            nextLevelText.enabled = false;
        }

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = true;
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER!!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void GOscreen()
    {

        GameOverScreen.Setup(totalCheese);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("PlayerHealth");
        PlayerPrefs.DeleteKey("PlayerSpeed");
    }

    public void RestartGame()
    {

        Application.LoadLevel(Application.loadedLevel);
    }
    public void EnableText()
    {
        nextLevelText.enabled = true;
        timeWhenDisappear = Time.time + timeToAppear;
    }

}
