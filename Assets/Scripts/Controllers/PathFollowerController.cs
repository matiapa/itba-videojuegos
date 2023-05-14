using UnityEngine;

public class PathFollowerController : MonoBehaviour {
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject pathContainer;

    private Transform[] _points;
    private int _pathIndex = 0;

    void Start() {
        if (pathContainer)
    	    SetPath(pathContainer);
    }
		
    void Update() {
        if (_points == null)
            return;

	    Vector3 dir = _points[_pathIndex].position - transform.position;
        
	    transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, _points[_pathIndex].position) <= 0.4f)
		    _pathIndex++;

        if (_pathIndex == _points.Length)
            Destroy(gameObject);
    }

    public void SetPath(GameObject _pathContainer) {
        pathContainer = _pathContainer;
        _points = new Transform[pathContainer.transform.childCount];
        for (int i = 0; i < _points.Length; i++)
	        _points[i] = pathContainer.transform.GetChild(i);
    }
}
