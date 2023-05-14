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
            GameObject[] enemies;
            
            if (_isEnemy)
                enemies = GameObject.FindObjectsOfType<Turret>().Select(enemy => enemy.gameObject).ToArray();
            else
                enemies = GameObject.FindObjectsOfType<Enemy>().Select(enemy => enemy.gameObject).ToArray();
            
            enemies = enemies
                .Where(enemy => Vector3.Distance(transform.position, enemy.transform.position) <= _maxRange)
                .ToArray();

            foreach(GameObject enemy in enemies) {
                Quaternion lookRotation = Quaternion.LookRotation(enemy.transform.position - transform.position);
                Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
                this.transform.rotation = Quaternion.Euler (0f, rotation.y, 0f);

                var bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);

                bullet.GetComponent<Bullet>().SetTarget(enemy);
            }
            
            _currentShotCooldown = _shotCooldown;
        }
    }

    public void SetIsEnemy(bool isEnemy) {
        _isEnemy = isEnemy;
    }
}