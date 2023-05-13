using Strategy;
using UnityEngine;
using UnityEngine;

public class BasicBullet : MonoBehaviour, IBullet
{
    public IDamageable Target => target;
    private IDamageable target;

    public float Speed => speed;
    [SerializeField] private float speed = 70f;

    public GameObject ImpactEffect => impactEffect;
    [SerializeField] private GameObject impactEffect;

    private void Update()
    {
        if (target == null)
        {
            DestroyBullet();
            return;
        }

        Travel();
    }

    public void Travel()
    {
        Vector3 dir = target.Transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            // target.TakeDamage(damage); ?????
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    public void OnCollisionEnter(Collision collision)
    {
        // TODO:
        // chequear layer para ver si es algo que deberiamos atacar y obtener el componente IDamageable
        // target.TakeDamage(damage);
        if (impactEffect != null)
        {
            GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 2.5f);
        }
        else
        {
            Debug.LogWarning("No impactEffect has been assigned to the bullet.");
        }

        if (target != null)
        {
            Destroy(target.Transform.gameObject);
        }

        DestroyBullet();
    }
    
    private void DestroyBullet()
    {
        Debug.Log("Bullet has been destroyed.");
        Destroy(gameObject);
    }
    
}
