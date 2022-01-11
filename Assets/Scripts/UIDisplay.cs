using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{

    [SerializeField] GameObject playerPrefab;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Slider slider;
    Health health;
    ScoreKeeper scoreKeeper;
    

    private void Awake()
    {
        health = playerPrefab.GetComponent<Health>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        slider.maxValue = health.GetHealth();
    }

 
    void Update()
    {
        slider.value = health.GetHealth();
        scoreText.text = "SCORE: " + scoreKeeper.GetScore().ToString();
    }
}
