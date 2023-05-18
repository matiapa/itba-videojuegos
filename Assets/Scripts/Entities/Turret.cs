using UnityEngine;

[RequireComponent(typeof(FillingLifeController))]
[RequireComponent(typeof(RangeAttackController))]
[RequireComponent(typeof(AudioSource))]
public class Turret : MonoBehaviour {

	private FillingLifeController _fillingLifeController;
    private RangeAttackController _rangeAttackController;
    public bool IsDead => _fillingLifeController.IsDead;
    public FillingLifeController A => _fillingLifeController;
    
    private AudioSource _audioSource;

    [SerializeField] private AudioClip _canonShot;

    void Awake() {
        _fillingLifeController = GetComponent<FillingLifeController>();

        _rangeAttackController = GetComponent<RangeAttackController>();
        _rangeAttackController.SetIsEnemy(false);

        _audioSource = GetComponent<AudioSource>();
    }

    void Start() {
        EventManager.instance.OnAttack += OnAttack;
    }

    void OnAttack(GameObject attacker) {
        if (attacker == this.gameObject)
            _audioSource.PlayOneShot(_canonShot);
    }
}
