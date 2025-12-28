using UnityEngine;

public class GhostStateMachine : EnemyStateMachine, IEdgeble
{
    private EnemyMovement movement;
    private EnemyChecks checks;
    private bool isNeedChangeOrientation;
    private bool isGotDamage;
    private bool isPushing;
    private int edgeOrientaiton;
    private PlayerTriggerType lastTriggerType;
    [SerializeField] private float pushingTime;
    [SerializeField] private Vector2 attackForce;

    protected override void OnEnable()
    {
        base.OnEnable();
        GetComponent<EnemyHPManager>().GotDamage += GotDamage;

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GetComponent<EnemyHPManager>().GotDamage -= GotDamage;
    }
    protected void GotDamage(PlayerTriggerType triggerType)
    {
        isGotDamage = true;
        lastTriggerType = triggerType;
    }
    protected override void Start()
    {
        base.Start();
        movement = GetComponent<EnemyMovement>();
        checks = GetComponent<EnemyChecks>();
    }

    private void Update()
    {
        if(isDied)
            ChangeState(EnemyState.Dead);
        else if (isGotDamage)
            ChangeState(EnemyState.GotDamage);
        else if (!isPushing)
        {
            if (state != EnemyState.Attack && checks.isPlayerNearRectangle())
                ChangeState(EnemyState.Attack);
            else if (state != EnemyState.Wander && !checks.isPlayerNearRectangle())
                ChangeState(EnemyState.Wander);
        }

        if(state == EnemyState.Attack && checks.isWrongOrientation())
        {
            isNeedChangeOrientation = false;
            orientation.ChangeOrientation();
        }
    }
    protected override void EnterState(EnemyState newState)
    {
        switch (newState)
        {
            case EnemyState.GotDamage:
                isGotDamage = false;
                isPushing = true;
                Invoke("ChangeIsPushing", pushingTime);
                movement.SetZeroLinearVelocity();
                movement.SetIsUpdateBlocked(true);
                if(lastTriggerType == PlayerTriggerType.Left)
                    movement.EnemyAddForce(new Vector2(-attackForce.x, 0));
                else if(lastTriggerType == PlayerTriggerType.Right)
                    movement.EnemyAddForce(new Vector2(attackForce.x, 0));
                else if(lastTriggerType == PlayerTriggerType.Up)
                    movement.EnemyAddForce(new Vector2(0, attackForce.y));
                else
                    movement.EnemyAddForce(new Vector2(0, -attackForce.y));
                break;
            case EnemyState.Attack:
                eAnimation.AttackAnimation(true);
                movement.SetRunSpeed();
                break;
            case EnemyState.Wander:
                isNeedChangeOrientation = false;
                movement.SetWalkSpeed();
                break;
            case EnemyState.Dead:
                movement.SetZeroSpeed();
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
            case EnemyState.GotDamage:
                movement.SetIsUpdateBlocked(false);
                if (isNeedChangeOrientation)
                    orientation.ChangeOrientation(edgeOrientaiton);
                movement.SetZeroLinearVelocity();
                break;
            case EnemyState.Attack:
                eAnimation.AttackAnimation(false);
                if (isNeedChangeOrientation && !isGotDamage)
                    orientation.ChangeOrientation(edgeOrientaiton); //else isNeedChangeOrientation = false 
                break;
        }
    }

    private void ChangeIsPushing()
    {
        isPushing = false;
    }

    public void OnEdge(int edgeOrientation)
    {
        if (state != EnemyState.Attack && state != EnemyState.GotDamage)
            orientation.ChangeOrientation(edgeOrientation);    
        else
        {
            this.edgeOrientaiton = edgeOrientation;
            isNeedChangeOrientation = true;
        }
    }
}
