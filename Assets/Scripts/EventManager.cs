using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    
    public static event Action<int, int> OnAmmoChanged;
    public static event Action<int> OnEnemyKilled;
    public static event Action<int> OnPlayerDamaged;
    public static event Action<string> OnPlaySound;
    public static event Action<int> OnScoreChanged;
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    public static void AmmoChanged(int current, int max)
    {
        OnAmmoChanged?.Invoke(current, max);
    }
    
    public static void EnemyKilled(int enemiesLeft)
    {
        OnEnemyKilled?.Invoke(enemiesLeft);
        OnPlaySound?.Invoke("EnemyDeath");
    }
    
    public static void PlayerDamaged(int health)
    {
        OnPlayerDamaged?.Invoke(health);
        OnPlaySound?.Invoke("PlayerHurt");
    }
    
    public static void PlaySound(string soundName)
    {
        OnPlaySound?.Invoke(soundName);
    }
    
    public static void ScoreChanged(int newScore)
    {
        OnScoreChanged?.Invoke(newScore);
    }
}