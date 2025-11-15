using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public List<string> weaponNames = new List<string>();
    public int activeWeaponIndex = -1;
    public Dictionary<string, int> consumables = new Dictionary<string, int>();
    public Vector3 playerPosition;
    public Vector3 playerRotation;
    public int score = 0;
    public int playerHealth = 100;
}

public class SaveSystem : MonoBehaviour
{
    private string savePath;
    
    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "savegame.json");
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        if (playerManager != null)
        {
            data.activeWeaponIndex = playerManager.GetActiveWeaponIndex();
            data.weaponNames = playerManager.GetWeaponNames();
        }
        
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory != null)
        {
            data.consumables = playerInventory.GetConsumables();
        }
        
        data.playerPosition = transform.position;
        data.playerRotation = transform.eulerAngles;
        
        if (ScoreManager.Instance != null)
            data.score = ScoreManager.Instance.GetScore();
            
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth != null)
            data.playerHealth = playerHealth.GetHealth();
        
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        
        Debug.Log("Game saved to: " + savePath);
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("No save file found");
            return;
        }
        
        string json = File.ReadAllText(savePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        if (playerManager != null)
        {
            playerManager.LoadWeapons(data.weaponNames, data.activeWeaponIndex);
        }
        
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory != null)
        {
            playerInventory.LoadConsumables(data.consumables);
        }
        
        transform.position = data.playerPosition;
        transform.eulerAngles = data.playerRotation;
        
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.SetScore(data.score);
            
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            int healthDiff = data.playerHealth - playerHealth.GetHealth();
            if (healthDiff > 0)
                playerHealth.Heal(healthDiff);
            else if (healthDiff < 0)
                playerHealth.TakeDamage(-healthDiff);
        }
        
        Debug.Log("Game loaded from: " + savePath);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
            SaveGame();
            
        if (Input.GetKeyDown(KeyCode.F9))
            LoadGame();
    }
}
