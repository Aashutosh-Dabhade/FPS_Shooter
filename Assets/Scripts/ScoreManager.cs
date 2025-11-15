using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    [Header("Score Settings")]
    public int damageScore = 10;
    public int killScore = 100;
    
    [Header("UI")]
    public Text scoreText;
    
    private int currentScore = 0;
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    void Start()
    {
        UpdateScoreUI();
    }
    
    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreUI();
        EventManager.ScoreChanged(currentScore);
    }
    
    public int GetScore()
    {
        return currentScore;
    }
    
    public void SetScore(int score)
    {
        currentScore = score;
        UpdateScoreUI();
        EventManager.ScoreChanged(currentScore);
    }
    
    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + currentScore;
    }
}