using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GunPickup : MonoBehaviour, ICollectible
{
    public Gun gunPrefab; 
    public string ItemName => gunPrefab != null ? gunPrefab.name : "Gun";

    public void Collect(PlayerInventory inventory)
    {
        Debug.Log($"Picked up gun: {ItemName}");

     
        PlayerManager player = inventory.GetComponent<PlayerManager>();
        if (player != null && gunPrefab != null)
        {
            player.AddWeapon(gunPrefab);
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
