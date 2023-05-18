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
        if (Physics.Raycast(ray, out hit, 1000))
        {
            IBuildHolder buildHolder = hit.transform.gameObject.GetComponent<IBuildHolder>();
            if (buildHolder != null)
            {
                actualPos = hit.transform.gameObject.transform.position;
            }
        }
 
    }

    private void Update()
    {
        if (pendingTurret == null)
            return;
        pendingTurret.transform.position = actualPos;
        
        if (Input.GetAxis("Click") <= 0)
            return;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out hit, 1000))
            return;

        IBuildHolder buildHolder = hit.transform.gameObject.GetComponent<IBuildHolder>();
        if (buildHolder == null)
            return;
        
        float distanceThreshold = 2.0f; // Umbral de distancia
        if (Vector3.Distance(hit.transform.gameObject.transform.position, actualPos) > distanceThreshold)
            return;

        int cost = turrets[index].gameObject.GetComponent<BuildController>().Cost;
        if (GameManager.instance.Coins - cost >= 0)
        {
            Destroy(pendingTurret);
            CmdBuild cmdBuild = new CmdBuild(buildHolder, turrets[index]);
            CommandQueue.instance.AddEventToQueue(cmdBuild);
            pendingTurret = null;
        }
        else
        {
            print("no te alcanza");
        }
        
    }

    public void SelectTurret(int index)
    {
        this.index = index;
        if(pendingTurret != null)
            Destroy(pendingTurret);
        pendingTurret = Instantiate(turretsPreviews[index], actualPos, transform.rotation);
    }
    
}
