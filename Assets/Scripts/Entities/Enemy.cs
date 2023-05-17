using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PathFollowerController))]
[RequireComponent(typeof(BasicLifeController))]
[RequireComponent(typeof(RangeAttackController))]
[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour {

    [SerializeField] private int _coinsEarnedOnDeath;
    [SerializeField] private int _livesRemovedOnArrival;
    [SerializeField] private AudioClip _deathSound;

    private PathFollowerController _pathFollowerController;
    private BasicLifeController _basicLifeController;
    private RangeAttackController _rangeAttackController;
    private AudioSource _audioSource;
    
    public bool IsDead => _basicLifeController.IsDead;

    void Awake() {
        _pathFollowerController = GetComponent<PathFollowerController>();

        _basicLifeController = GetComponent<BasicLifeController>();
        _basicLifeController.OnDeath += OnDeath;

        _rangeAttackController = GetComponent<RangeAttackController>();
        _rangeAttackController.SetIsEnemy(true);

        _audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (_pathFollowerController.endReached) {
            EventManager.instance.LivesChange(-_livesRemovedOnArrival);
            Destroy(gameObject);
        }
    }

    private void OnDeath() {
        EventManager.instance.CoinChange(_coinsEarnedOnDeath);

        _audioSource.PlayOneShot(_deathSound);
        // TODO: animacion de muerte
        this._rangeAttackController.enabled = false;
        this.transform.position = new Vector3(this.transform.position.x, -200, transform.position.z);
        Invoke("DestroyEnemy", 2f);
    }

    private void DestroyEnemy() {
        Destroy(this.gameObject);
    }

    public void SetPath(GameObject _pathContainer) {
       _pathFollowerController.SetPath(_pathContainer);
    }
}
