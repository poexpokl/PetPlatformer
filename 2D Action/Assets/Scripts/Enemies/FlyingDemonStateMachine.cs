using System.Collections;
using UnityEngine;

public class FlyingDemonStateMachine : EnemyStateMachine
{
    [SerializeField] private float teleportTime;
    private bool isTeleporting;
    private EnemyTeleportation teleport;
    private EnemyCreateObject attack;
    private Transform playerTransform;
    private EnemyChecks checks;

    protected override void Start()
    {
        base.Start();
        teleport = GetComponent<EnemyTeleportation>();
        attack = GetComponent<EnemyCreateObject>();
        playerTransform = G.playerTransform;
        checks = GetComponent<EnemyChecks>();
    }

    private void Update()
    {
        if(isDied) 
            ChangeState(EnemyState.Dead);
        else if (!isTeleporting)
        {
            if (checks.isPlayerNearCircle())
                ChangeState(EnemyState.Teleport);
            else if (checks.isPlayerActivateRectangle() && checks.CanSeePlayer() && !attack.isCooldown)
                ChangeState(EnemyState.Attack);
            else if (state != EnemyState.Stay && !attack.isAttacking)
                ChangeState(EnemyState.Stay);

            if (checks.isPlayerActivateRectangle() && checks.isWrongOrientation())
                orientation.ChangeOrientation();
        }
    }
    protected override void EnterState(EnemyState newState)
    {
        switch (newState)
        {
            case EnemyState.Attack:
                eAnimation.AttackAnimation(true);
                attack.CreateObject(playerTransform);
                break;
            case EnemyState.Teleport:
                isTeleporting = true;
                eAnimation.TeleportAnimation(true);
                StartCoroutine(Teleporting());
                break;
            //case EnemyState.Stay:
            //    break;
            case EnemyState.Dead:
                eAnimation.DeadAnimation();
                attack.StopCoroutines();
                GetComponentInChildren<EnemyTrigger>().gameObject.SetActive(false);
                GetComponent<DestroyAtfter>().DestroyObject();
                gameObject.tag = "Untagged";
                StopAllCoroutines();
                enabled = false;
                break;
        }
    }

    protected override void ExitState(EnemyState newState)
    {
        switch (newState) 
        {
            case EnemyState.Attack:
                if (attack.isAttacking)
                    attack.StopCoroutines();
                eAnimation.AttackAnimation(false);
                break;
            case EnemyState.Teleport:
                eAnimation.TeleportAnimation(false);
                break;


        }
    }

    IEnumerator Teleporting()
    {
        yield return new WaitForSeconds(teleportTime);
        isTeleporting = false;
        teleport.Teleportation();
        teleport.ChangeTeleportationX();
    }
}
