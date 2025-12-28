using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class SceletonStateMachine : EnemyStateMachine, IEdgeble 
{
    private PlayerTriggerType lastTriggerType;
    [SerializeField] private float pushingTime;
    [SerializeField] private Vector2 attackForce;
    private bool isGotDamage;
    private bool isPushing;
    private EnemyMovement movement;

    protected override void Start()
    {
        base.Start();
        movement = GetComponent<EnemyMovement>();
    }

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

    private void Update()
    {
        if(isDied)
            ChangeState(EnemyState.Dead);
        else if (isGotDamage)
            ChangeState(EnemyState.GotDamage);
        else if (!isPushing && state != EnemyState.Move)
            ChangeState(EnemyState.Move);
    }

    protected override void EnterState(EnemyState newState)
    {
        switch (newState)
        {
            case EnemyState.Move:
                movement.SetRunSpeed();
                break;
            case EnemyState.GotDamage:
                isGotDamage = false;
                if (lastTriggerType == PlayerTriggerType.Left || lastTriggerType == PlayerTriggerType.Right)
                {
                    isPushing = true;
                    Invoke("ChangeIsPushing", pushingTime);
                    movement.SetZeroLinearVelocity();
                    movement.SetIsUpdateBlocked(true);
                    if (lastTriggerType == PlayerTriggerType.Left)
                        movement.EnemyAddForce(new Vector2(-attackForce.x, 0));
                    else
                        movement.EnemyAddForce(new Vector2(attackForce.x, 0));
                }
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

    private void ChangeIsPushing()
    {
        isPushing = false;
    }
    protected override void ExitState(EnemyState oldState)
    {
        switch (oldState)
        {
            case EnemyState.GotDamage:
                movement.SetIsUpdateBlocked(false);
                break;
        }
    }
    public void OnEdge(int edgeOrientation)
    {
        orientation.ChangeOrientation(edgeOrientation);
    }
}
