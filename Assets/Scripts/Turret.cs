using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour {

	private Transform target;
	public float range = 30f;
	public string enemyTag = "Enemy";
	private Transform pivot;
	public float turnSpeed = 10f;
	
	void Start () {
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
		pivot = this.transform;
	}
	
	void UpdateTarget ()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		var closestEnemy = enemies
			.Select(enemy => (Enemy: enemy, Distance: Vector3.Distance(transform.position, enemy.transform.position)))
			.Where(x => x.Distance <= range)
			.OrderBy(x => x.Distance)
			.FirstOrDefault();

		if (closestEnemy.Enemy != null)
		{
			target = closestEnemy.Enemy.transform;
		}
		else
		{
			target = null;
		}

	}

	// Update is called once per frame
	void Update () {
		if (target == null)
			return;
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(pivot.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		pivot.rotation = Quaternion.Euler (0f, rotation.y, 0f);
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
