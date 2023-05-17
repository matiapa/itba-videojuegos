using System.Collections;
using Microlight.MicroBar;
using UnityEngine;

public class BasicLifeController : MonoBehaviour, IDamageable {

    [SerializeField] protected float _maxLife = 100f;

    [SerializeField] MicroBar _hpBar;

    private bool _isDeath;

    protected float _currentLife;

    public float MaxLife => _maxLife;
    public float CurrentLife => _currentLife;

    public bool IsDeath => _isDeath;

    void Start() {
        _currentLife = _maxLife;
        _isDeath = false;
        
        if (_hpBar != null) 
            _hpBar.Initialize(_maxLife);
    }

    public void TakeDamage(float damage) {
        _currentLife -= damage;

        if (_hpBar != null)
            _hpBar.UpdateHealthBar(_currentLife);

        if (_currentLife <= 0)
        {
            _isDeath = true;
            EventManager.instance.EntityDeath(this.gameObject);
        }

        // print(this.gameObject.name+" - Current life: "+_currentLife);
    }
}