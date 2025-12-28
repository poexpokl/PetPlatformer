using UnityEngine;

public class BringerOfDeathStateMachine : EnemyStateMachine
{
    private EnemyCreateObject attack;
    private EnemyChecks checks;
    private EnemyCheckPlayerSprite checkPlayerSprite;
    private Transform playerTransform;

    protected override void Start()
    {
        base.Start();
        attack = GetComponent<EnemyCreateObject>();
        checks = GetComponent<EnemyChecks>();
        checkPlayerSprite = GetComponent<EnemyCheckPlayerSprite>();
        playerTransform = G.playerTransform;
    }

    private void Update()
    {
        if(isDied)
            ChangeState(EnemyState.Dead);
        else if (checks.isPlayerActivateRectangle() && !attack.isCooldown) //Rectangle?
            ChangeState(EnemyState.Attack);
        else if (!attack.isAttacking && state != EnemyState.Stay)
            ChangeState(EnemyState.Stay);

        if (checks.isPlayerActivateRectangle() && checks.isWrongOrientation())
            orientation.ChangeOrientation();
    }
    protected override void EnterState(EnemyState newState)
    {
        switch (newState)
        {
            case EnemyState.Attack:
                eAnimation.AttackAnimation(true);
                int multiplier = checkPlayerSprite.isPlayerFlipX() ? -1 : 1;
                attack.CreateObject(playerTransform, multiplier);
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

    protected override void ExitState(EnemyState newState)
    {
        switch (newState)
        {
            case EnemyState.Attack:
                eAnimation.AttackAnimation(false);
                break;
        }
    }
}
