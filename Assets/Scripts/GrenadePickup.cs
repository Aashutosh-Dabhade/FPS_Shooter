using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GrenadePickup : MonoBehaviour, ICollectible
{
    public GrenadeWeapon grenadePrefab;
    public string ItemName => grenadePrefab != null ? grenadePrefab.name : "Grenade";

    public void Collect(PlayerInventory inventory)
    {
        Debug.Log($"Picked up grenade: {ItemName}");

        PlayerManager player = inventory.GetComponent<PlayerManager>();
        if (player != null && grenadePrefab != null)
        {
            player.AddWeapon(grenadePrefab);
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
