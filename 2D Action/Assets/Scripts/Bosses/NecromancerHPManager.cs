using System;
using UnityEngine;

public class NecromancerHPManager : MonoBehaviour, IDamageble
{
    [SerializeField] float hp;
    private float startHP;
    public event Action<PlayerTriggerType> GotDamage;
    public event Action Died;
    public event Action EnterNewPhase;
    private bool enteredPhase2;
    private bool enteredPhase3;
    private bool isInvulnerable;
    private void Start()
    {
        startHP = hp;
    }
    public void GetDamage(float damage, PlayerTriggerType triggerType)
    {
        if (!isInvulnerable)
        {
            hp -= damage;
            if (hp > 0)
            {
                GotDamage?.Invoke(triggerType); //для звука
                if (hp < 0.5f * startHP && !enteredPhase2)
                {
                    enteredPhase2 = true;
                    EnterNewPhase?.Invoke();
                }
            }
            else
            {
                if (!enteredPhase3)
                {
                    enteredPhase3 = true;
                    EnterNewPhase?.Invoke();
                    SetVulnerability(true);
                }
                else
                    Died?.Invoke();
            }
        }
    }

    public void SetVulnerability(bool b)
    {
        isInvulnerable = b;
    }
}
