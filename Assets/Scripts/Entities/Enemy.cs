using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PathFollowerController))]
[RequireComponent(typeof(BasicLifeController))]
[RequireComponent(typeof(RangeAttackController))]
public class Enemy : MonoBehaviour {

    private PathFollowerController _pathFollowerController;
    private BasicLifeController _basicLifeController;
    private RangeAttackController _rangeAttackController;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _deathSound;
    private bool _isDeath;
    public bool IsDeath => _basicLifeController.IsDeath;

    void Awake() {
        _pathFollowerController = GetComponent<PathFollowerController>();
        _basicLifeController = GetComponent<BasicLifeController>();
        _rangeAttackController = GetComponent<RangeAttackController>();

        _rangeAttackController.SetIsEnemy(true);
        _audioSource = GetComponent<AudioSource>();
        EventManager.instance.OnEntityDeath += OnEntityDeath;
    }

    public void SetPath(GameObject _pathContainer) {
       _pathFollowerController.SetPath(_pathContainer);
    }
    
    void OnEntityDeath(GameObject entity) {
        if (entity == this.gameObject)
        {
            _audioSource.PlayOneShot(_deathSound);
            // TODO: animacion de muerte
            this._rangeAttackController.enabled = false;
            this.transform.position = new Vector3(this.transform.position.x, -200, transform.position.z);
            Invoke("EnemyDeath", 2f);
        }
    }

    void EnemyDeath()
    {
        EventManager.instance.OnEntityDeath -= OnEntityDeath;
        Destroy(this.gameObject);
    }
}
