using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public GameObject[] turretsPreviews;
    public GameObject[] turrets;
    private int index;
    private GameObject pendingTurret;
    private RaycastHit hit;
    private Vector3 actualPos;
    [SerializeField] private LayerMask layerMask;

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            // Get the node
            actualPos = hit.transform.gameObject.transform.position;
        }
    }

    private void Update()
    {
        if (pendingTurret == null)
            return;
        pendingTurret.transform.position = actualPos;
        
        if (!Input.GetMouseButtonDown(0))
            return;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out hit, 1000, layerMask))
            return;

        float distanceThreshold = 2.0f; // Umbral de distancia
        if (Vector3.Distance(hit.transform.gameObject.transform.position, actualPos) > distanceThreshold)
            return;

        Vector3 lastPos = pendingTurret.transform.position;
        Quaternion lastRot = pendingTurret.transform.rotation;
        Destroy(pendingTurret);
        Instantiate(turrets[index], lastPos, lastRot);
        pendingTurret = null;
    }




    public void SelectTurret(int index)
    {
        this.index = index;
        if(pendingTurret != null)
            Destroy(pendingTurret);
        pendingTurret = Instantiate(turretsPreviews[index], actualPos, transform.rotation);
    }
    
}
