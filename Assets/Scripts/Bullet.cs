using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
    public float speed = 50f;
    public float lifeTime = 3f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 direction)
    {
        rb.linearVelocity = direction.normalized * speed;
        Invoke(nameof(DisableBullet), lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        IDamageable damageable = collision.collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }

        DisableBullet();
    }

    private void OnTriggerEnter(Collider other)
    {
       
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }

        DisableBullet();
    }

    private void DisableBullet()
    {
        rb.linearVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
