using UnityEngine;

namespace Strategy
{
    public interface IDamageable
    {
        int MaxLife { get; }
        int CurrentLife { get; }
        Transform Transform { get; }

        void TakeDamage(int damage);
    }
}