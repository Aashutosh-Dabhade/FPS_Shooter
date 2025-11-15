using UnityEngine;

public enum CollectibleType
{
    Consumable,
    Weapon
}

public class CollectibleItem : MonoBehaviour, ICollectible
{
    [Header("General Settings")]
    [SerializeField] private string itemName;
    [SerializeField] private CollectibleType type;
    [SerializeField] private int amount = 1; 

    [Header("Weapon (only if type = Weapon)")]
    public Weapon weaponPrefab; 

    public string ItemName => !string.IsNullOrEmpty(itemName)
        ? itemName
        : weaponPrefab != null ? weaponPrefab.weaponData.weaponName : "Unknown";

    public void Collect(PlayerInventory inventory)
    {
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;
        switch (type)
        {
            case CollectibleType.Consumable:
                inventory.AddConsumable(ItemName, amount);
                Debug.Log($"Picked up consumable: {ItemName} (+{amount})");
                break;

            case CollectibleType.Weapon:
                if (weaponPrefab != null)
                {
                    inventory.AddWeapon(weaponPrefab);
                    Debug.Log($"Picked up weapon: {ItemName}");
                }
                break;
        }

        Destroy(gameObject);
    }

  private void OnTriggerEnter(Collider other)
{
    PlayerManager player = other.GetComponent<PlayerManager>();
    if (player != null && type == CollectibleType.Weapon && weaponPrefab != null)
    {
        player.AddWeapon(weaponPrefab); // ✅ goes to PlayerManager inventory
        Destroy(gameObject);
        return;
    }

    PlayerInventory inventory = other.GetComponent<PlayerInventory>();
    if (inventory != null)
    {
        Collect(inventory);
    }
}

}
