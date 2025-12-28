using System;
using UnityEngine;

public class EnemyHPManager : MonoBehaviour, IDamageble
{
    [SerializeField] float hp;
    public event Action<PlayerTriggerType> GotDamage;
    public event Action Died;
    public void GetDamage(float damage, PlayerTriggerType triggerType)
    {
        hp -= damage;
        if(hp > 0)
        {
            GotDamage?.Invoke(triggerType);
        }
        else
        {
            Died?.Invoke();
        }
    }
}
