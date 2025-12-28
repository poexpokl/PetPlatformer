using UnityEngine;

public class WizardSummonerStateMachine : EnemyStateMachine
{
    private EnemyCreateObject attack;
    private EnemyChecks checks;

    protected override void Start()
    {
        base.Start();
        attack = GetComponent<EnemyCreateObject>();
        checks = GetComponent<EnemyChecks>();
    }

    private void Update()
    {
        if(isDied)
            ChangeState(EnemyState.Dead);
        else if(checks.isPlayerActivateRectangle() && !attack.isCooldown)
            ChangeState(EnemyState.Attack);
        else if(!checks.isPlayerActivateRectangle() && state != EnemyState.Stay)
            ChangeState(EnemyState.Stay);

        if(checks.isPlayerActivateRectangle() && checks.isWrongOrientation())
            orientation.ChangeOrientation();
    }
    protected override void EnterState(EnemyState newState)
    {
        switch (newState) 
        {
            case EnemyState.Attack:
                eAnimation.AttackAnimation(true);
                attack.CreateObject();
                break;
            case EnemyState.Dead:
                eAnimation.DeadAnimation();
                GetComponentInChildren<EnemyTrigger>().gameObject.SetActive(false);
                gameObject.tag = "Untagged";
                GetComponent<DestroyAtfter>().DestroyObject();
                enabled = false;
                break;
        }
    }

    protected override void ExitState(EnemyState oldState)
    {
        switch (oldState)
        {
            case EnemyState.Attack:
                eAnimation.AttackAnimation(false);
                if (attack.isCooldown)
                    attack.StopCoroutines();
                break;
        }
    }
}
