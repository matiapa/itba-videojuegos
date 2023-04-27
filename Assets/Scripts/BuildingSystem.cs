using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public GameObject[] turrets;
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
        if (pendingTurret != null)
        {
            pendingTurret.transform.position = actualPos;
            if (Input.GetMouseButtonDown(0))
                pendingTurret = null;
        }
    }

    public void SelectTurret(int index)
    {
        // habria que modificar esto para no instanciar torres al pedo, y tener algun vector donde guardemos las torres
        // construidas
        pendingTurret = Instantiate(turrets[index], actualPos, transform.rotation);
    }
    
}
