using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Text cheeseText;
    public void Setup(int score)
    {
        cheeseText.text = "You collected " + score + " cheese!";
        gameObject.SetActive(true);
    }
}