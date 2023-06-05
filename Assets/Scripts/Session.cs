using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Session : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI playerLivesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int scorePerCoin = 100; 
    public int currentPlayerLives;
    int currentScore = 0;

    [Obsolete]
    private void Awake()
    {
        int countSession = FindObjectsOfType<Session>().Length;
        if (countSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        currentPlayerLives = playerLives;
        playerLivesText.text = currentPlayerLives.ToString();
        scoreText.text = currentScore.ToString();
    }
    public void ProcessPlayerDeath()
    {
        if (currentPlayerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void ResetGameSession()
    {
        currentPlayerLives = playerLives;
        currentScore = 0;
        FindObjectOfType<ScenePersist>().ResetScenePresist();
        SceneManager.LoadScene(0);
        playerLivesText.text = currentPlayerLives.ToString();
        scoreText.text = currentScore.ToString();
    }

    private void TakeLife()
    {
        currentPlayerLives--;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
        playerLivesText.text = currentPlayerLives.ToString();
    }
    public void AddToScore()
    {
        currentScore += scorePerCoin;
        scoreText.text = currentScore.ToString();
    }
}
