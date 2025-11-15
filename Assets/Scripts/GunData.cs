using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewGun", menuName = "Guns/Gun Data")]
public class GunData : ScriptableObject
{
    [Header("Basic Info")]
    public string gunName;
    public GameObject bulletPrefab;


    [Header("Stats")]
    public int damage = 10;
    public float fireRate = 0.2f;
    public float reloadTime = 2f;
    public int magazineSize = 10;

    [Header("Recoil & Accuracy")]
    public float recoil = 1f;
    public float bulletSpread = 0.05f;
    [Header("Range")]
    public float bulletSpeed = 50f;
    public float maxRange = 100f;

    [Header("Animation")]
    public bool Aiming = false;
    public string shootAnimation;
    public bool Shooting = false;

    [Header("Audio")]
    public AudioClip FireAudio;
    public AudioClip EmptyAudio;

    public Weapon weaponType;

    public Sprite GunImage;
    public Sprite icon;

}
