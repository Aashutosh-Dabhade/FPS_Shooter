using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class GrenadeWeapon : Weapon
{
    public GrenadeData grenadeData;
    public Transform throwPoint;

    public PlayerManager playerManager;
    Animator playeranim;

    AimManager aimManager;
    private bool canThrow = true;

    public Weapon weapon;
    public Sprite grenadesprite;
    void Start()
    {
//        weapon.weaponData.weaponType = WeaponType.Grenade;
    }
    public override void Use()
    {
        if (!canThrow || grenadeData == null) return;
        ThrowGrenade();
    }

    private void ThrowGrenade()
    {
        playeranim = playerManager.GetComponent<Animator>();
        aimManager = playerManager.GetComponent<AimManager>();
        playeranim.SetTrigger("Throw");
        StartCoroutine(throwsequence());
        
    }

    private System.Collections.IEnumerator ResetThrowCooldown()
    {
        playerManager.animator.SetLayerWeight(0,1);
        playerManager.animator.SetLayerWeight(1,0);

        yield return new WaitForSeconds(1f); // cooldown before next throw
        canThrow = true;
    }

    IEnumerator throwsequence()
    {
        playerManager.animator.SetLayerWeight(0,0);
        playerManager.animator.SetLayerWeight(1,1);

        yield return new WaitForSeconds(1.8f);
        GameObject grenade = Instantiate(grenadeData.grenadePrefab.gameObject, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = grenade.AddComponent<Rigidbody>();
        rb.useGravity = true;
        grenade.transform.SetParent(throwPoint.transform);
       
        if (rb != null)
        {
            //   Vector3 throwDir = throwPoint.forward * grenadeData.throwForce + throwPoint.up * grenadeData.upwardForce;
            Vector3 throwDir = throwPoint.forward * grenadeData.throwForce + throwPoint.up * grenadeData.upwardForce;
            rb.AddForce(throwDir, ForceMode.VelocityChange);
            grenade.transform.SetParent(null);
        }

        Grenade grenadeScript = grenade.GetComponent<Grenade>();
        if (grenadeScript != null)
        {
            grenadeScript.Setup(grenadeData); // pass scriptable object
        }
        canThrow = false;
        StartCoroutine(ResetThrowCooldown());
    }
}
