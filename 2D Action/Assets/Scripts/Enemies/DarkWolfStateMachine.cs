using UnityEngine;

public class DarkWolfStateMachine : EnemyStateMachine, IEdgeble
{
    private EnemyMovement movement;
    [SerializeField] private Vector2 reAttackVector; //название
    [SerializeField] private float speedOnAttack;
    private EnemyChecks checks;

    protected override void Start()
    {
        checks = GetComponent<EnemyChecks>();
        movement = GetComponent<EnemyMovement>();
        orientation = GetComponent<EnemyOrientation>();
        eAnimation = GetComponent<EnemyAnimation>();
    }

    private void Update()
    {
        if(isDied)
            ChangeState(EnemyState.Dead);
        else if (!checks.isPlayerActivateRectangle())
        {
            ChangeState(EnemyState.Wander);
        }
        else if ((state == EnemyState.Wander || state == EnemyState.Attack) && !checks.isPlayerNearRectangle())
        {
            ChangeState(EnemyState.Move);
        }
        else if (state == EnemyState.Move && checks.isPlayerNearRectangle())
        {
            ChangeState(EnemyState.Attack);
        }
        else if (state == EnemyState.Attack && !checks.isPlayerNearRectangle(reAttackVector.x, reAttackVector.y) && checks.isWrongOrientation())
        {
            orientation.ChangeOrientation();
            ChangeState(EnemyState.Attack);
        }
        else if (state == EnemyState.Stay && checks.isWrongOrientation())
        {
            orientation.ChangeOrientation();
            if (checks.isPlayerNearRectangle())
            {   
                ChangeState(EnemyState.Attack);
            }
            else
            {
                ChangeState(EnemyState.Move);
            }
        }
        if (state == EnemyState.Move && checks.isWrongOrientation())
            orientation.ChangeOrientation();
    }

    protected override void ExitState(EnemyState wolfState)
    {
        switch (wolfState)
        {
            case EnemyState.Attack:
                eAnimation.AttackAnimation(false);
                break;
            case EnemyState.Move:
                eAnimation.RunAnimation(false);
                break;
            case EnemyState.Wander:
                eAnimation.WalkAnimation(false);
                break;
            case EnemyState.Stay:
                eAnimation.StayAnimation(false);
                break;
            case EnemyState.Dead:
                break;
        }
    }

    protected override void EnterState(EnemyState wolfState)
    {
        switch (wolfState)
        {
            case EnemyState.Attack:
                movement.SetSpeed(speedOnAttack);
                eAnimation.AttackAnimation(true);
                break;
            case EnemyState.Move:
                movement.SetRunSpeed();
                eAnimation.RunAnimation(true);
                break;
            case EnemyState.Wander:
                movement.SetWalkSpeed();
                eAnimation.WalkAnimation(true);
                break;
            case EnemyState.Stay:
                movement.SetZeroSpeed();
                eAnimation.StayAnimation(true);
                break;
            case EnemyState.Dead:
                movement.SetZeroSpeed();
                eAnimation.DeadAnimation();
                GetComponentInChildren<EnemyTrigger>().gameObject.SetActive(false);
                gameObject.tag = "Untagged"; //
                GetComponent<DestroyAtfter>().DestroyObject();
                enabled = false;
                break;
        }
    }

    public void OnEdge(int edgeOrientation) //как обязать поставить триггеры? Переименовать т.к. вызывается и при упоре в стену
    {
        if (state == EnemyState.Wander)
            orientation.ChangeOrientation(edgeOrientation);
        else
        {
            ChangeState(EnemyState.Stay);
        }
    }
}
