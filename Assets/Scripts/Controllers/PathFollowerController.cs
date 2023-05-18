using UnityEngine;
public class PathFollowerController : MonoBehaviour, ISlowable {
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject pathContainer;

    private Transform[] _points;
    private int _pathIndex = 0;
    private float _currentSpeed;

    public bool endReached => _pathIndex == _points.Length;

    void Start() {
        if (pathContainer)
    	    SetPath(pathContainer);
        _currentSpeed = speed;
    }
		
    void Update() {
        if (_points == null || _pathIndex == _points.Length)
            return;

	    Vector3 dir = _points[_pathIndex].position - transform.position;
        
	    transform.Translate(dir.normalized * _currentSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, _points[_pathIndex].position) <= 0.4f)
		    _pathIndex++;
    }

    public void SetPath(GameObject _pathContainer) {
        pathContainer = _pathContainer;
        _points = new Transform[pathContainer.transform.childCount];
        for (int i = 0; i < _points.Length; i++)
	        _points[i] = pathContainer.transform.GetChild(i);
    }

    public void TakeSlowEffect(float _slowFactor, float _slowDuration)
    {
	    if(_currentSpeed == speed)
	    {
		    print("aplico slow, speed antes: " + _currentSpeed);
		    _currentSpeed *= _slowFactor;
		    print("aplico slow, speed despues: " + _currentSpeed);
		    Invoke("ResetSpeed", _slowDuration);
	    }
    }

    private void ResetSpeed()
    {
	    print("reseteo speed ");
	    _currentSpeed = speed;
    }
}
