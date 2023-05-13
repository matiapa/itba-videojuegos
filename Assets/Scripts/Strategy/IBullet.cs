using Unity.VisualScripting;
using UnityEngine;

namespace Strategy
{
    public interface IBullet
    {
        float Speed { get;  }
        
        // animacion o efecto al impactar
        GameObject ImpactEffect { get; }
        
        IDamageable Target { get; }
        
        void Travel();

        void OnCollisionEnter(Collision collision);
    }
}