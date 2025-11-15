using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponData weaponData;

    public virtual void Use()
    {
        Debug.Log("Using weapon: " + weaponData.weaponName);
    }
}
