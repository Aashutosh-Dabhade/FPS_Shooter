using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class Gun : Weapon  
{
    public GunData gunData;          
    public Transform firePoint;      
    public ObjectPool bulletPool;   
    public Transform LeftHandPosition;
    public Transform RightHandPosition;
    
    private AudioSource audioSource;
    private int bulletsLeft;
    private bool canShoot = true;

    private float bulletScale = 1f;
    public Sprite gunsprite;

    private void Start()
    {
        gunData.GunImage = gunsprite;
        SetMagazine();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void SetMagazine()
    {
        bulletsLeft = gunData.magazineSize;
        EventManager.AmmoChanged(bulletsLeft, gunData.magazineSize);
    }

    public override void Use()  
    {
        Shoot();
    }

    public void Shoot()
    {
        if (!canShoot)
            return;

        if (bulletsLeft <= 0)
        {
            PlaySound(gunData.EmptyAudio);
            return;
        }

       
        Vector3 shootDirection = firePoint.up;
        shootDirection.x += Random.Range(-gunData.bulletSpread, gunData.bulletSpread);
        shootDirection.y += Random.Range(-gunData.bulletSpread, gunData.bulletSpread);

       
        FireBullet(shootDirection);

        gunData.Shooting = true;
        bulletsLeft--;
        EventManager.AmmoChanged(bulletsLeft, gunData.magazineSize);

        PlaySound(gunData.FireAudio);
        EventManager.PlaySound("GunShot");

        canShoot = false;
        StartCoroutine(ShootCooldown());
    }

    private void FireBullet(Vector3 direction)
    {
        GameObject bulletObj = bulletPool.GetObject();
        bulletObj.transform.position = firePoint.position;
        bulletObj.transform.rotation = Quaternion.LookRotation(direction);
      //  bulletObj.transform.localScale = Vector3.one * bulletScale;

        bulletObj.SetActive(true);

        
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.damage = gunData.damage;
            bullet.speed = gunData.bulletSpeed;
            bullet.Launch(direction);
        }
        else
        {
            Rigidbody rb = bulletObj.GetComponent<Rigidbody>();
            if (rb != null)
                rb.linearVelocity = direction.normalized * gunData.bulletSpeed;
        }
    }

    private IEnumerator ShootCooldown()
    {
        gunData.Shooting = false;
        yield return new WaitForSeconds(gunData.fireRate);
        canShoot = true;
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void Reload()
    {
        Debug.Log($"🔄 Reloading {gunData.gunName}");
        bulletsLeft = gunData.magazineSize;
        EventManager.AmmoChanged(bulletsLeft, gunData.magazineSize);
        EventManager.PlaySound("Reload");
    }
    
    public void SetBulletSize(float size)
    {
        bulletScale = Mathf.Max(0.0001f, size);
    }
}
