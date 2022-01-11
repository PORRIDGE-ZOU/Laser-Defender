using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    static ScoreKeeper instance;//singleton pattern
    private int score = 0;

    private void Awake()
    {
        CheckForSingleton();
    }

    void CheckForSingleton()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int score)
    {
        this.score += score;
    }

    public void ResetScore()
    {
        score = 0;
    }


}
