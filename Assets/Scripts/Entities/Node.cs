using UnityEngine;

public class Node : MonoBehaviour, IBuildHolder
{
    public void PlaceBuild(GameObject buildable)
    {
        buildable.GetComponent<BuildController>().Build(this.transform);
    }
}
