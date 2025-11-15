using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
   // public Text ammoText;
    //public Text healthText;
    public Text enemyCountText;
    public Text scoreText;
    
    void OnEnable()
    {
      
        EventManager.OnEnemyKilled += UpdateEnemyCountUI;
      
        EventManager.OnScoreChanged += UpdateScoreUI;
    }
    
    void OnDisable()
    {
     
        EventManager.OnEnemyKilled -= UpdateEnemyCountUI;
      
        EventManager.OnScoreChanged -= UpdateScoreUI;
    }
    
    
    void UpdateEnemyCountUI(int enemiesLeft)
    {
        if (enemyCountText != null)
            enemyCountText.text = $"Enemies: {enemiesLeft}";
    }
    
    void UpdateScoreUI(int score)
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }
}