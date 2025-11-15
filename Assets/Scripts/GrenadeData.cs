using UnityEngine;

[CreateAssetMenu(menuName = "Game/GrenadeData")]
public class GrenadeData : ScriptableObject
{
  public string grenadeName = "Grenade";
  public Sprite icon;

  [Header("Grenade Stats")]
  public int damage = 50;
  public float throwForce = 15f;
  public float upwardForce = 4f;
  public float explosionDelay = 3f;
  public float explosionRadius = 8f;
  public float explosionForce = 5f;

  [Header("Prefabs & FX")]
  public Grenade grenadePrefab;
  public GameObject explosionEffect;

  public Weapon weapontype;

  public Sprite GrenadeImage;
}
