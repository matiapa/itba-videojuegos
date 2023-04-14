using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	private Transform path;
    private static Transform[] _points;
    private Transform target;
    private int pathIndex;
    private Enemy enemy;
    
    void Awake ()
    {
	    path = GameObject.Find("Middle path").GetComponent<Transform>();
	    pathIndex = 0;
    	_points = new Transform[path.childCount];
        for (int i = 0; i < _points.Length; i++)
	        _points[i] = path.transform.GetChild(i);
    }
    
    void Start()
    {
	    enemy = GetComponent<Enemy>();
	    target = _points[pathIndex];
    }
		
    void Update()
    {
	    Vector3 dir = target.position - transform.position;
	    transform.Translate(dir.normalized * enemy.Speed * Time.deltaTime, Space.World);

	    if (Vector3.Distance(transform.position, target.position) <= 0.4f)
	    {
		    if (pathIndex >= _points.Length - 1)
		    {
			    Destroy(gameObject);
			    return;
		    }
		    target = _points[++pathIndex];
	    }
    }
}
