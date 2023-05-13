using UnityEngine;

public interface IBullet {

    float Damage { get; }

    float Speed { get;  }
    
    float LifeTime { get; }

    GameObject ImpactEffect { get; }
    
    Vector3 Target { get; }

    void Travel();

    void OnCollisionEnter(Collision collision);

    void SetTarget(Vector3 target);
}