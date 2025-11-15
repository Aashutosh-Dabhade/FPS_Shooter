using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MeleePickup : MonoBehaviour, ICollectible
{
    public MeleeWeapon meleePrefab;
    public string ItemName => meleePrefab != null ? meleePrefab.name : "Melee";

    public void Collect(PlayerInventory inventory)
    {
        Debug.Log($"Picked up melee weapon: {ItemName}");

        PlayerManager player = inventory.GetComponent<PlayerManager>();
        if (player != null && meleePrefab != null)
        {
            player.AddWeapon(meleePrefab);
        }

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Destroy(gameObject, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if (inventory != null)
        {
            Collect(inventory);
        }
    }
}
