using UnityEngine;

public class BasicLifeController : MonoBehaviour, IDamageable {

    [SerializeField] protected float _maxLife = 100f;

    protected float _currentLife;

    public float MaxLife => _maxLife;
    public float CurrentLife => _currentLife;

    void Start() {
        _currentLife = _maxLife;
    }

    public void TakeDamage(float damage) {
        _currentLife -= damage;
        if (_currentLife <= 0)
            Destroy(this.gameObject);
        print(this.gameObject.name+" - Current life: "+_currentLife);
    }
}