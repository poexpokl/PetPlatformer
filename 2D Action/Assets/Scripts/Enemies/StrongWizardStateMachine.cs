using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class StrongWizardStateMachine : EnemyStateMachine
{
    private EnemyChecks checks;
    private EnemyCreateObject attack;
    [SerializeField] private EnemyCreateObject attack2;

    protected override void Start()
    {
        base.Start();
        checks = GetComponent<EnemyChecks>();
        attack = GetComponent<EnemyCreateObject>();
    }
    private void Update()
    {
        if(isDied) 
            ChangeState(EnemyState.Dead);
        else if (checks.isPlayerActivateRectangle() && !attack.isCooldown && checks.CanSeePlayer())
            ChangeState(EnemyState.Attack);
        else if(state != EnemyState.Stay && !attack.isAttacking)
            ChangeState(EnemyState.Stay);

        if(checks.isPlayerActivateRectangle() && checks.isWrongOrientation())
            orientation.ChangeOrientation();
    }
    protected override void EnterState(EnemyState newState)
    {
        switch (newState)
        {
            case EnemyState.Attack:
                attack.CreateObject(1); //orientaiton
                attack2.CreateObject(1);
                attack.CreateFlippedObject(1);
                attack2.CreateFlippedObject(1);
                eAnimation.AttackAnimation(true);
                break;
            case EnemyState.Stay:
                //eAnimation.StayAnimation(true);
                break;
            case EnemyState.Dead:
                eAnimation.DeadAnimation();
                attack.StopCoroutines();
                GetComponentInChildren<EnemyTrigger>().gameObject.SetActive(false);
                gameObject.tag = "Untagged"; //
                GetComponent<DestroyAtfter>().DestroyObject();
                enabled = false;
                break;
        }
    }
    protected override void ExitState(EnemyState newState)
    {
        switch (newState)
        {
            case EnemyState.Attack:
                eAnimation.AttackAnimation(false);
                break;
            case EnemyState.Stay:
                //eAnimation.StayAnimation(false);
                break;
        }
    }
}
