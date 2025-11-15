using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class MeleeWeapon : Weapon
{
    public MeleeData meleeData;
    private float nextAttackTime = 2f;
    private AudioSource audioSource;

    
    public Transform attackOrigin;

    public PlayerManager playerManager;

    public Sprite meleesprite;

    [Header("Cooldown Handling")]
    public UnityEvent onCooldown; 

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        nextAttackTime = Time.time + (1f / Mathf.Max(0.0001f, meleeData.attackRate));
        if (meleeData == null && weaponData != null)
        {
            Debug.LogWarning(
                $"MeleeWeapon on '{name}' expected a MeleeData assigned to weaponData but found {weaponData.GetType().Name}. " +
                $"Create a MeleeData asset and assign it.", this);
        }
    }

    public override void Use()
    {
        if (meleeData == null) return;

        if (Time.time < nextAttackTime)
        {
            HandleCooldown();
            return;
        }

        nextAttackTime = Time.time + (1f / Mathf.Max(0.0001f, meleeData.attackRate));
        PerformAttack();
    }

    private void HandleCooldown()
    {
        StartCoroutine(cooldown());
        
       
    }

    IEnumerator cooldown()
    {


        yield return new WaitForSeconds(meleeData.attackRate);
        playerManager.animator.SetLayerWeight(0, 0);
        playerManager.animator.SetLayerWeight(1, 1);
        yield return new WaitForSeconds(1);
        playerManager.animator.SetLayerWeight(0, 1);
        playerManager.animator.SetLayerWeight(1, 0);


    }

    private void PerformAttack()
    {
        Debug.Log("Melee action performed");

        if (playerManager != null && playerManager.animator != null)
        {
            playerManager.animator.SetTrigger("Attack");
            playerManager.animator.SetLayerWeight(0, 0);
            playerManager.animator.SetLayerWeight(1, 1);
        }

        if (meleeData.swingSound != null)
        {
            audioSource.clip = meleeData.swingSound;
            audioSource.Play();
        }

        Vector3 center = (attackOrigin != null)
            ? attackOrigin.position
            : transform.position + transform.forward * (meleeData.range * 0.5f);

        Collider[] hits = Physics.OverlapSphere(center, meleeData.range);

        bool anyHit = false;
        foreach (var col in hits)
        {
            if (col.transform.IsChildOf(transform) || col.gameObject == gameObject) continue;

            IDamageable damageable = col.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(meleeData.damage);
                anyHit = true;
            }

            if (anyHit && meleeData.hitEffect != null)
            {
                Vector3 spawnPos = col.ClosestPoint(center);
                Instantiate(meleeData.hitEffect, spawnPos, Quaternion.identity);
            }
        }

        if (anyHit && meleeData.hitSound != null)
        {
            audioSource.PlayOneShot(meleeData.hitSound);
        }
       StartCoroutine(cooldown());
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (meleeData != null)
        {
            Vector3 center = (attackOrigin != null)
                ? attackOrigin.position
                : (transform.position + transform.forward * (meleeData.range * 0.5f));
            Gizmos.color = new Color(1, 0, 0, 0.35f);
            Gizmos.DrawSphere(center, meleeData.range);
        }
    }
#endif
}
