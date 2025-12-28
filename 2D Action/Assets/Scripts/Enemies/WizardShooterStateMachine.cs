using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class WizardShooterStateMachine : EnemyStateMachine, IEdgeble
{
    private EnemyCreateObject attack;
    private EnemyChecks checks;
    private EnemyMovement movement;
    private Transform playerTransform;
    protected override void Start()
    {
        base.Start();
        attack = GetComponent<EnemyCreateObject>();
        movement = GetComponent<EnemyMovement>();
        checks = GetComponent<EnemyChecks>();
        playerTransform = G.playerTransform;
    }
    private void Update()
    {
        if (isDied)
            ChangeState(EnemyState.Dead);
        else if (!attack.isCooldown)
        {
            if (checks.isPlayerActivateRectangle())
            {
                if (checks.CanSeePlayer())
                    ChangeState(EnemyState.Attack);
                else
                    ChangeState(EnemyState.Move);
            }
            else
                ChangeState(EnemyState.Wander);
        }
        else if(attack.isAttacking && checks.isWrongOrientation())
            orientation.ChangeOrientation();
        else if(!attack.isAttacking)
            ChangeState(EnemyState.Stay);

    }
    protected override void EnterState(EnemyState newState)
    {
        switch (newState)
        {
            case EnemyState.Move:
                movement.SetRunSpeed();
                eAnimation.RunAnimation(true);
                break;
            case EnemyState.Wander:
                movement.SetWalkSpeed();
                eAnimation.WalkAnimation(true);
                break;
            case EnemyState.Attack:
                attack.CreateObject(playerTransform); 
                eAnimation.AttackAnimation(true);
                movement.SetZeroSpeed();
                break ;
            case EnemyState.Stay:
                movement.SetZeroSpeed();
                eAnimation.StayAnimation(true);
                break;
            case EnemyState.Dead:
                attack.StopCoroutines();
                movement.SetZeroSpeed();
                eAnimation.DeadAnimation();
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
            case EnemyState.Move:
                eAnimation.RunAnimation(false);
                break;
            case EnemyState.Wander:
                eAnimation.WalkAnimation(false);
                break;
            case EnemyState.Attack:
                eAnimation.AttackAnimation(false);
                break;
            case EnemyState.Stay:
                eAnimation.StayAnimation(false);
                break;
        }
    }

    public void OnEdge(int edgeOrientation) 
    {
        if (state == EnemyState.Move || state == EnemyState.Wander)
            orientation.ChangeOrientation(edgeOrientation);
    }
}
