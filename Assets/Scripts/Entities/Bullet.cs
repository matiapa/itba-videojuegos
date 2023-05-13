using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifetime = 5f;
    [SerializeField] private List<int> _layerMasks;
    [SerializeField] private GameObject _impactEffect;

    private Vector3 _target;

    public float Damage => _damage;
    public float Speed => _speed;
    public float LifeTime => _lifetime;
    public GameObject ImpactEffect => _impactEffect;
    public Vector3 Target => _target;

    public void Travel() {
        Vector3 dir = _target - transform.position;
        
	    transform.Translate(dir.normalized * _speed * Time.deltaTime, Space.World);
    }

    public void OnCollisionEnter(Collision collision) {
        if (!_layerMasks.Contains(collision.gameObject.layer))
            return;

        IDamageable damagable = collision.gameObject.GetComponent<IDamageable>();
        if (damagable != null)
            EventQueueManager.instance.AddEvent(new CmdApplyDamage(damagable, _damage));

        if (_impactEffect != null) {
            GameObject effectIns = Instantiate(_impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 2.5f);
        }

        Destroy(this.gameObject);
    }

    void Update() {
        Travel();

        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0)
            Destroy(this.gameObject);
    }

    public void SetTarget(Vector3 newTarget) {
        _target = newTarget;
    }
}