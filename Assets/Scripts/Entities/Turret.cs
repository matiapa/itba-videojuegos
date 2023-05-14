using UnityEngine;

[RequireComponent(typeof(FillingLifeController))]
[RequireComponent(typeof(RangeAttackController))]
public class Turret : MonoBehaviour {

	private FillingLifeController _fillingLifeController;
    private RangeAttackController _rangeAttackController;

    void Start() {
        _fillingLifeController = GetComponent<FillingLifeController>();
        _rangeAttackController = GetComponent<RangeAttackController>();

        _rangeAttackController.SetIsEnemy(false);
    }
}
