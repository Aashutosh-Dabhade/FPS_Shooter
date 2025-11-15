using UnityEngine;

public enum WeaponType
{
    Gun,
    Melee,
    Grenade
}

[CreateAssetMenu(menuName = "Game/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public Sprite icon;

    [Header("Stats")]
    public int damage;
    public float fireRate;
    public int magazineSize;
    
    public bool isPrimary;

    [Header("Type")]
    public WeaponType weaponType;   
}
