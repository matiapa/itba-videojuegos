using UnityEngine;

[RequireComponent(typeof(FillingLifeController))]
[RequireComponent(typeof(RangeAttackController))]
[RequireComponent(typeof(AudioSource))]
public class Turret : MonoBehaviour {

	private FillingLifeController _fillingLifeController;
    private RangeAttackController _rangeAttackController;
    public bool IsDead => _fillingLifeController.IsDead;
    
    private AudioSource _audioSource;

    [SerializeField] private AudioClip _canonShot;

    void Start() {
        _fillingLifeController = GetComponent<FillingLifeController>();
        _rangeAttackController = GetComponent<RangeAttackController>();
        _audioSource = GetComponent<AudioSource>();
        _rangeAttackController.SetIsEnemy(false);

        EventManager.instance.OnAttack += OnAttack;
    }

    void OnAttack(GameObject attacker) {
        if (attacker == this.gameObject)
            _audioSource.PlayOneShot(_canonShot);
    }
}
