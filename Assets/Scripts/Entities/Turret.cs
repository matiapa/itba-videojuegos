using UnityEngine;

[RequireComponent(typeof(FillingLifeController))]
[RequireComponent(typeof(RangeAttackController))]
[RequireComponent(typeof(AudioSource))]
public class Turret : MonoBehaviour {

	private FillingLifeController _fillingLifeController;
    private RangeAttackController _rangeAttackController;
    private bool _isDeath;
    public bool IsDeath => _fillingLifeController.IsDeath;
    
    [SerializeField] private AudioClip _canonShot;
    private AudioSource _audioSource;

    void Start() {
        _fillingLifeController = GetComponent<FillingLifeController>();
        _rangeAttackController = GetComponent<RangeAttackController>();
        _audioSource = GetComponent<AudioSource>();
        _isDeath = false;
        _rangeAttackController.SetIsEnemy(false);

        EventManager.instance.OnAttack += OnAttack;
    }

    void OnAttack(GameObject attacker) {
        if (attacker == this.gameObject)
            _audioSource.PlayOneShot(_canonShot);
    }
}
