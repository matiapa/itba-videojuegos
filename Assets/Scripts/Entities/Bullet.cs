using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _speed = 30f;
    [SerializeField] private float _lifetime = 5f;
    [SerializeField] private GameObject _impactEffect;

    private GameObject _target;

    public float Damage => _damage;
    public float Speed => _speed;
    public float LifeTime => _lifetime;
    public GameObject ImpactEffect => _impactEffect;
    public GameObject Target => _target;

    public void Travel() {
        if(_target == null) {
            Destroy(this.gameObject);
            return;
        }
        
        Vector3 dir = _target.transform.position - transform.position;
        
        if (dir.magnitude > 1)
	        transform.Translate(dir.normalized * _speed * Time.deltaTime, Space.World);
        else if (dir.magnitude > 0)
            transform.Translate(dir, Space.World);
    }

    public void OnTriggerEnter(Collider collider) {
        IDamageable damagable = collider.gameObject.GetComponent<IDamageable>();
        if (damagable != null) {
            EventQueueManager.instance.AddEvent(new CmdApplyDamage(damagable, _damage));
        }

        if (_impactEffect != null) {
            GameObject effectIns = Instantiate(_impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 2.5f);
        }
    }

    void Update() {
        Travel();

        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0)
            Destroy(this.gameObject);
    }

    public void SetTarget(GameObject newTarget) {
        _target = newTarget;
    }
}