using UnityEngine;

[System.Serializable]
public class Wave {

    public GameObject enemy;
    public GameObject path;
    public int count;
    public float rate;
    
    public float Duration {
    	get { return count / rate; }
    }
    
}
