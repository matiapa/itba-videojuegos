using UnityEngine;

[RequireComponent(typeof(PathFollowerController))]
[RequireComponent(typeof(BasicLifeController))]
[RequireComponent(typeof(RangeAttackController))]
public class Enemy : MonoBehaviour {

    private PathFollowerController _pathFollowerController;
    private BasicLifeController _basicLifeController;
    private RangeAttackController _rangeAttackController;

    void Awake() {
        _pathFollowerController = GetComponent<PathFollowerController>();
        _basicLifeController = GetComponent<BasicLifeController>();
        _rangeAttackController = GetComponent<RangeAttackController>();

        _rangeAttackController.SetIsEnemy(true);
    }

    public void SetPath(GameObject _pathContainer) {
       _pathFollowerController.SetPath(_pathContainer);
    }
}
