using System.Collections;
using Microlight.MicroBar;
using UnityEngine;

public class BasicLifeController : MonoBehaviour, IDamageable {

    [SerializeField] protected float _maxLife = 100f;

    [SerializeField] MicroBar _hpBar;

    protected float _currentLife;

    public float MaxLife => _maxLife;
    public float CurrentLife => _currentLife;

    void Start() {
        _currentLife = _maxLife;

        if (_hpBar != null) 
            _hpBar.Initialize(_maxLife);
    }

    public void TakeDamage(float damage) {
        _currentLife -= damage;

        if (_hpBar != null)
            _hpBar.UpdateHealthBar(_currentLife);

        if (_currentLife <= 0) 
            Destroy(this.gameObject);
            
        // print(this.gameObject.name+" - Current life: "+_currentLife);
    }
}