using UnityEngine;
public class CmdApplyPoisonDamage : MonoBehaviour, ICommand
{
    private IDamageable _damageable;
    private float _damage;
    private int _numTimes;
    private float _repeatInterval;

    private int _currentApplicationCount;

    public CmdApplyPoisonDamage(IDamageable damageable, float damage, int numTimes, float repeatInterval)
    {
        _damageable = damageable;
        _damage = damage;
        _numTimes = numTimes;
        _repeatInterval = repeatInterval;
    }

    public void Execute()
    {
        InvokeRepeating(nameof(ApplyPoisonDamage), 0f, _repeatInterval);
        _currentApplicationCount = 0;
    }

    private void ApplyPoisonDamage()
    {
        _damageable.TakeDamage(_damage);
        _currentApplicationCount++;

        if (_currentApplicationCount >= _numTimes)
        {
            CancelInvoke(nameof(ApplyPoisonDamage));
        }
    }
}