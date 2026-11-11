using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] float startTime = 5f;
    [SerializeField] float timeToAdd = 5f;
    float timeLeft = 5f;
    bool gameOver = false;

    public bool GameOver => gameOver;

    private void Start()
    {
        timeLeft = startTime;
    }

    private void Update()
    {
        DecraseTime();
    }

    private bool DecraseTime()
    {
        if (gameOver) return false;
        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F1");
        if (timeLeft <= 0)
        {
            PlayerGameOver();
        }

        return true;
    }

    void PlayerGameOver()
    {
        gameOver = true;
        playerController.enabled = false;
        gameOverText.SetActive(true);
        Time.timeScale = 0.1f;
    }
    public void IncreaseTime(float amount)
    {
        timeLeft += amount;
    }
}
