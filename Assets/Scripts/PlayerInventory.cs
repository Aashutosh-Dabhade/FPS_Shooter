using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Transform weaponHolder;

    private List<Weapon> weapons = new List<Weapon>();
    private Dictionary<string, int> consumables = new Dictionary<string, int>();

    private int activeWeaponIndex = -1;
    private Weapon currentWeapon;

    // --- New: add consumables ---
    public void AddConsumable(string itemName, int amount)
    {
        if (!consumables.ContainsKey(itemName))
            consumables[itemName] = 0;

        consumables[itemName] += amount;

        Debug.Log($"Added consumable: {itemName}, Total = {consumables[itemName]}");
        // TODO: Update UI here (e.g. consumable slots)
    }

    public int GetConsumableCount(string itemName)
    {
        return consumables.ContainsKey(itemName) ? consumables[itemName] : 0;
    }

    public bool UseConsumable(string itemName)
    {
        if (consumables.ContainsKey(itemName) && consumables[itemName] > 0)
        {
            consumables[itemName]--;
            Debug.Log($"Used 1 {itemName}, Remaining = {consumables[itemName]}");
            return true;
        }
        return false;
    }

    // --- Weapons ---
    public void AddWeapon(Weapon weaponPrefab)
    {
        Weapon newWeapon = Instantiate(weaponPrefab, weaponHolder);
        newWeapon.gameObject.SetActive(false);

        weapons.Add(newWeapon);

        if (weapons.Count == 1)
            EquipWeapon(0);
    }

    public void EquipWeapon(int index)
    {
        if (index < 0 || index >= weapons.Count) return;

        if (currentWeapon != null)
            currentWeapon.gameObject.SetActive(false);

        currentWeapon = weapons[index];
        currentWeapon.gameObject.SetActive(true);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;

        activeWeaponIndex = index;
    }

    private void Update()
    {
        if (currentWeapon != null && Input.GetMouseButton(0))
            currentWeapon.Use();

        if (Input.GetKeyDown(KeyCode.Tab) && weapons.Count > 0)
            EquipWeapon((activeWeaponIndex + 1) % weapons.Count);
    }

    public Dictionary<string, int> GetConsumables()
    {
        return new Dictionary<string, int>(consumables);
    }

    public void LoadConsumables(Dictionary<string, int> loadedConsumables)
    {
        consumables.Clear();
        foreach (var item in loadedConsumables)
        {
            consumables[item.Key] = item.Value;
        }
    }
}
