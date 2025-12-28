using System.Collections;
using UnityEngine;

public class NecromancerMain : MonoBehaviour
{
    [SerializeField] private float AbsoluteAttackSpeed;
    private NecromancerMovement movement;
    private NecromancerAttacks attacks;
    private int currentPositionPoint = 1;
    private int phase = 1;
    private int randomNumber;
    private int absoluteLaserCount = 0;
    private int absoluteLaserMaxCount = 10;
    private Animator animator;
    private void Start()
    {
        movement = GetComponent<NecromancerMovement>();
        attacks = GetComponent<NecromancerAttacks>();
        int seed = System.DateTime.Now.Millisecond;
        animator = GetComponent<Animator>();
        Random.InitState(seed);
        StartCoroutine(AttackEndFrame());
    }
    private void OnEnable()
    {
        GetComponent<NecromancerMovement>().Moved += Attack;
        GetComponent<NecromancerAttacks>().Attaked += Move;
        GetComponent<NecromancerHPManager>().EnterNewPhase += EnterNewPhase;
        GetComponent<NecromancerHPManager>().Died += Die;
    }

    private void OnDisable()
    {
        GetComponent<NecromancerMovement>().Moved -= Attack;
        GetComponent<NecromancerAttacks>().Attaked -= Move;
        GetComponent<NecromancerHPManager>().EnterNewPhase -= EnterNewPhase;
        GetComponent<NecromancerHPManager>().Died -= Die;
    }

    private void Attack()
    {
        if (phase == 1)
        {
            randomNumber = Random.Range(0, 3);
            if (randomNumber == 0)
                attacks.VerticalInsideLaser();
            else if (randomNumber == 1)
                attacks.VerticalOutsideLaser();
            else
                attacks.VerticalChessLaser();
        }
        else if (phase == 2)
        {
            if(currentPositionPoint != 3)
            {
                randomNumber = Random.Range(0, 5);
                if (randomNumber == 0)
                    attacks.VerticalInsideLaser();
                else if (randomNumber == 1)
                    attacks.VerticalOutsideLaser();
                else if(randomNumber == 2)
                    attacks.VerticalChessLaser();
                else if(randomNumber == 3)
                    attacks.HorizontalDownLaser();
                else
                    attacks.HorizontalUpLaser();
            }
            else
            {
                if (currentPositionPoint == 3)
                    attacks.SkullAttack();
                else
                    Move();
            }
        }
        else
        {
            if (absoluteLaserCount == 0)
                attacks.ChangeLasersAnimationSpeed(AbsoluteAttackSpeed);
            if (currentPositionPoint != 3) //?
                Move();
            else if(absoluteLaserCount < absoluteLaserMaxCount / 2)
            {
                randomNumber = Random.Range(0, 6);
                attacks.AbsoluteLaser(randomNumber);
            }
            else
            {
                randomNumber = Random.Range(0, 6);
                int randomNumber2 = Random.Range(6, 8);
                attacks.AbsoluteLaser(randomNumber, randomNumber2);
            }
            absoluteLaserCount++;
        }
    }

    IEnumerator AttackEndFrame()
    {
        yield return new WaitForEndOfFrame();
        Attack();
    }
    private void Move()
    {
        //Возможно добавить задержку перед этим
        if(phase == 1)
        {
            randomNumber = Random.Range(0, 2);
            if (randomNumber >= currentPositionPoint)
                randomNumber++;
            currentPositionPoint = randomNumber;
            movement.MoveToPoint(currentPositionPoint);
        }
        else if(phase == 2)
        {
            if(currentPositionPoint == 3)
            {
                randomNumber = Random.Range(0, 3);
                currentPositionPoint = randomNumber;
                movement.MoveToPoint(currentPositionPoint);
            }
            else
            {
                float randomFloatNumber = Random.Range(0f, 1f);
                if (randomFloatNumber <= 10f / 12)
                {
                    if (randomFloatNumber <= 5f / 12)
                        randomNumber = 0;
                    else
                        randomNumber = 1;
                    if (randomNumber >= currentPositionPoint)
                        randomNumber++;
                    currentPositionPoint = randomNumber;
                    movement.MoveToPoint(currentPositionPoint);
                }
                else
                {
                    randomNumber = 3;
                    currentPositionPoint = randomNumber;
                    movement.MoveToPoint(currentPositionPoint);
                }
            }
        }
        else
        {
            if(currentPositionPoint != 3) //?
            {
                currentPositionPoint = 3;
                movement.MoveToPoint(currentPositionPoint);
            }
            else if (absoluteLaserCount < absoluteLaserMaxCount)
                Attack();
            else
            {
                animator.SetBool("isGetDamage", true);
                Invoke("ChangeGetDamage", 0.25f);
                movement.MoveToPoint(4);
                GetComponent<NecromancerMovement>().Moved -= Attack;
                GetComponent<NecromancerAttacks>().Attaked -= Move;
                GetComponent<NecromancerHPManager>().SetVulnerability(false);
                GetComponentInChildren<EnemyTrigger>().gameObject.SetActive(false);
            }
        }
    }

    private void EnterNewPhase()
    {
        phase++;
    }
    private void ChangeGetDamage()
    {
        animator.SetBool("isGetDamage", false);
    }

    private void Die()
    {
        animator.SetBool("isDead", true);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<DestroyAtfter>().DestroyObject();
    }
}
