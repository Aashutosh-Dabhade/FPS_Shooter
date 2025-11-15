using UnityEngine;

public class Grenade : MonoBehaviour
{
    private GrenadeData data;
    private float countdown;
    private bool exploded = false;

    public void Setup(GrenadeData grenadeData)
    {
        data = grenadeData;
        countdown = data.explosionDelay;
    }

    private void Update()
    {
        if (data == null) return;

        countdown -= Time.deltaTime;
        if (countdown <= 0f && !exploded)
        {
            Explode();
        }
    }

    private void Explode()
    {
        exploded = true;

        // Spawn explosion FX
        if (data.explosionEffect != null)
            Instantiate(data.explosionEffect, transform.position, Quaternion.identity);

        // Find all colliders in range
        Collider[] colliders = Physics.OverlapSphere(transform.position, data.explosionRadius);

        foreach (Collider nearby in colliders)
        {
            // Apply damage if possible
            IDamageable dmg = nearby.GetComponent<IDamageable>();
            if (dmg != null)
                dmg.TakeDamage(data.damage);

            // Apply explosion force if object has Rigidbody
            Rigidbody rb = nearby.attachedRigidbody;
            if (rb != null)
            {
                // Force proportional to distance (closer = stronger)
                float distance = Vector3.Distance(transform.position, rb.position);
                float distanceFactor = Mathf.Clamp01(1 - (distance / data.explosionRadius));

                float force = data.explosionForce * distanceFactor;

                rb.AddExplosionForce(force, transform.position, data.explosionRadius, 1f, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }
}
