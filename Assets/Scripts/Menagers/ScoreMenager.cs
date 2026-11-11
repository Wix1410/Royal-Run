using UnityEngine;
using TMPro;

public class ScoreMenager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TMP_Text scoreText;

    int score = 0;

    public void IncreaseScore(int amount)
    {
        if (gameManager.GameOver) return;
        score += amount;
        scoreText.text = score.ToString();  
    }
}
