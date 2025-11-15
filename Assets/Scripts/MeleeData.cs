using UnityEngine;

[CreateAssetMenu(menuName = "Game/MeleeData")]
public class MeleeData : ScriptableObject
{
    [Header("General")]
    public string meleeName = "Knife";
    public Sprite icon;
    public GameObject hitEffect;

    [Header("Stats")]
    public int damage = 25;
    public float attackRate = 1f;  // attacks per second
    public float range = 1f;       // how far it can hit

    [Header("Audio")]
    public AudioClip swingSound;
    public AudioClip hitSound;

    public Weapon weapontype;

    public Sprite MeleeImage;
}
