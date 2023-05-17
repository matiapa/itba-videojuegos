using System.Linq;
using UnityEngine;

public class RangeAttackController : MonoBehaviour {

    [SerializeField] private float _maxRange = 50f;
    [SerializeField] private float _shotCooldown = 2f;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private bool _isEnemy;

    private int _currentBulletCount;
    private float _currentShotCooldown;


    private void Start() {
        _currentShotCooldown = _shotCooldown;
    }

    private void Update() {
        if (_currentShotCooldown >= 0)
            _currentShotCooldown -= Time.deltaTime;
        
        if (_currentShotCooldown <= 0) {
            GameObject nearestEnemy;
            
            if (_isEnemy)
                nearestEnemy = GameObject.FindObjectsOfType<Turret>()
                    .Where(enemy => !enemy.IsDead && Vector3.Distance(transform.position, enemy.transform.position) <= _maxRange)
                    .Select(enemy => enemy.gameObject)
                    .FirstOrDefault();
            else
                nearestEnemy = GameObject.FindObjectsOfType<Enemy>()
                    .Where(enemy => !enemy.IsDead && Vector3.Distance(transform.position, enemy.transform.position) <= _maxRange)
                    .Select(enemy => enemy.gameObject)
                    .FirstOrDefault();
            
            if (nearestEnemy == null)
                return;

            Quaternion lookRotation = Quaternion.LookRotation(nearestEnemy.transform.position - transform.position);
            Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
            this.transform.rotation = Quaternion.Euler (0f, rotation.y, 0f);

            var bullet = Instantiate(_bulletPrefab, transform.position + Vector3.forward, transform.rotation);

            bullet.GetComponent<Bullet>().SetTarget(nearestEnemy);

            EventManager.instance.Attack(this.gameObject);
            
            _currentShotCooldown = _shotCooldown;
        }
    }

    public void SetIsEnemy(bool isEnemy) {
        _isEnemy = isEnemy;
    }
}