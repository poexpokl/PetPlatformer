using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public PlayerState currentState { get; private set; } = PlayerState.Idle;
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float verticalSpeed = 10f;
    [SerializeField] private float dashSpeed = 25f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 0.3f;
    public PlayerState previousState { get; private set; } 
    private PlayerState nextState;
    private bool canGroundDash = true;
    private bool canAirDash = true;
    public bool isStateRepeat { get; private set; }
    private int playerOrientation = 1; //player?
    private int dashType; //0 - по земле, 1 - по воздуху
    private int healCounts = 0; // Нейинг
    private bool canJumping = true;
    private bool canAttack = true;
    private bool isUncontrollable = false;
    private bool isInvulnerable = false;
    private bool isDashing = false;
    private bool isAttacking = false;
    [SerializeField] private float uncontrollableTimeFromDamage = 0.1f; //мб нейминг
    [SerializeField] private float invulnerableTimeFromDamage = 0.3f;
    [SerializeField] private float jumpingTime = 0.3f;
    [SerializeField] private float attackCooldown = 0.2f;
    [SerializeField] private float attackTime = 0.2f;
    [SerializeField] private Vector2 botAirAttackForce = Vector2.up * 300; //нейминг
    [SerializeField] private Vector2 attackForce = Vector2.right * 1000; //нейминг 1000
    [SerializeField] private Vector2 getDamageForce;
    private float getDamageOrientation;
    private IInteractable interactable;
    public float playerMoveInput { get; private set; } //нейминг?
    private bool inInteractiveZone;
    private float jumpTimer = 0f;
    private float healTimer = 0f;
    [SerializeField] private float manaLoseTime;
    [SerializeField] private float deadTime = 2f;
    private bool needToCancelEffects = false; // сменить имя
    private bool isHealing;
    private bool isHitEnemy = false;
    private GroundCheck groundCheck;
    private AttackHitboxActivator attackHitboxActivator;
    private SpriteRenderer spriteRenderer;
    private ResourcesManager resourcesManager;
    private CeilingCheck ceilingCheck;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;
    private InputAction airBotAttackAction;
    private InputAction topAttackAction;
    private InputAction dashAction;
    private InputAction healAction;
    private InputAction interactAction;

    private bool isAttackPressed;
    private bool isAirBotAttackPressed;
    private bool isTopAttackPressed;
    public enum PlayerState
    {
        Idle,
        Run,
        Jump,
        Fall,
        Dash,
        Attack,
        TopAttack,
        AirAttack,
        AirBotAttack,
        AirTopAttack,
        Heal,
        GetDamage,
        Interact,
        Dead
    }

    private void Awake()
    {
        G.player = gameObject;
        G.playerTransform = transform;
        G.playerController = this;

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        attackAction = InputSystem.actions.FindAction("Attack");
        airBotAttackAction = InputSystem.actions.FindAction("AirBotAttack");
        topAttackAction = InputSystem.actions.FindAction("TopAttack");
        dashAction = InputSystem.actions.FindAction("Dash");
        healAction = InputSystem.actions.FindAction("Heal");
        interactAction = InputSystem.actions.FindAction("Interact");
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<GroundCheck>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        attackHitboxActivator = GetComponent<AttackHitboxActivator>();
        resourcesManager = GetComponent<ResourcesManager>();
        ceilingCheck = GetComponent<CeilingCheck>();
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        attackAction.Enable();
        airBotAttackAction.Enable();
        topAttackAction.Enable();
        dashAction.Enable();
        healAction.Enable();
        interactAction.Enable();

        attackAction.started += AttackPressed;
        airBotAttackAction.started += AirBotAttackPressed;
        topAttackAction.started += TopAttackPressed;
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        attackAction.Disable();
        airBotAttackAction.Disable();
        topAttackAction.Disable();
        dashAction.Disable();
        healAction.Disable();
        interactAction.Disable();

        attackAction.started -= AttackPressed;
        airBotAttackAction.started -= AirBotAttackPressed;
        topAttackAction.started -= TopAttackPressed;
    }

    private void Update()
    {
        playerMoveInput = moveAction.ReadValue<float>();
        if (isDashing == false && isHealing == false)
        {
            if (playerMoveInput > 0.01)
            {
                spriteRenderer.flipX = false;
                playerOrientation = 1;
            }
            else if (playerMoveInput < -0.01)
            {
                spriteRenderer.flipX = true;
                playerOrientation = -1;
            }
        }
        ChangeState();
        isAttackPressed = false;//...
        isAirBotAttackPressed = false;
        isTopAttackPressed = false;

        //Debug.Log(currentState);  
    }

    private void FixedUpdate()
    {// мб придётся вынести в отдельный метод
        if (needToCancelEffects)
        {
            CanselEffects();
            needToCancelEffects = false; //мб сменить название
        }
        if (currentState != PlayerState.Dash && currentState != PlayerState.Heal && !isUncontrollable)
            rb.linearVelocityX = moveAction.ReadValue<float>() * horizontalSpeed;
        else if (currentState == PlayerState.Heal)
            rb.linearVelocityX = 0;
        switch (currentState)
        {
            case PlayerState.Jump:
                rb.linearVelocityY = verticalSpeed;
                break;
            case PlayerState.Fall:
                //rb.linearVelocityY = -verticalSpeed;
                //мб поставить макс скорость
                break;
            case PlayerState.Dash:
                rb.linearVelocityY = 0;
                rb.linearVelocityX = dashSpeed * playerOrientation;
                break;
            //case "Heal": (linearVelocityX = 0)
            case PlayerState.GetDamage:
                rb.linearVelocity = Vector2.zero; //Вот это сделать единоразовым
                rb.AddForce(new Vector2(getDamageForce.x * getDamageOrientation, getDamageForce.y)); //мб это не один раз прокает?
                break;
            case PlayerState.Dead:
                rb.linearVelocityX = 0;
                break;
        }
    }
    private void Idle()
    {
        jumpTimer = 0;
        canJumping = true;
    }
    private void Run() //то же что Idle?
    {
        jumpTimer = 0;
        canJumping = true;
    }
    private void Jump()
    {
        jumpTimer += Time.deltaTime;
        if (jumpTimer > jumpingTime || ceilingCheck.Check())
        {
            canJumping = false;
        }
    }

    private void Fall()
    {
        jumpTimer = 0;//?
    }

    private void Dash(int dashType)
    {
        canJumping = false; //?


        isDashing = true;
        if (dashType == 0)
            canGroundDash = false;
        else if (dashType == 1)
            canAirDash = false;
        StartCoroutine(Dashing(dashType));
    }

    private void Dead()
    {
        InputSystem.actions.FindActionMap("Player").Disable(); //Нужно отключить скрипт с получением инпутов
        isInvulnerable = true;
        StartCoroutine(ReloadScene());
        enabled = false;
        //нужно отрубать скрипт со сменой state, но это когда я разделю их
        //if(!GroundCheck()) rb.linearVelocityY...
        //включить скрипт экрана смерти
        //enabled = false;
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(deadTime);

        SaveLoad.Instance.Load();
    }
    private void Interact()
    {
        interactable.HideInput();
        interactable.DoInteraction();
    }

    private bool CanInteract() //public??
    {
        if (inInteractiveZone && currentState == PlayerState.Idle)
            return true;
        else
            return false;
    }

    public void GetDamage()
    {
        if (!isInvulnerable)
        {
            resourcesManager.ChangeHp(-1);//изменить 1
            if (resourcesManager.hp == 0)
                Dead();
            else
            {
                getDamageOrientation = 0;
                isUncontrollable = true;
                StartCoroutine(EndUncontrollable());
                isInvulnerable = true;
                StartCoroutine(EndInvulnerable());
            }
        }
    }
    public void GetDamage(Vector2 enemyPosition) //нужно число снятый хп в параметры
    {
        if (!isInvulnerable)
        {
            resourcesManager.ChangeHp(-1);//изменить 1
            if (resourcesManager.hp == 0)
                Dead();
            else
            {
                if (enemyPosition.x > transform.position.x)
                    getDamageOrientation = -1;
                else
                    getDamageOrientation = 1;
                //тут как-то вычисляется как в связи с enemyPosition получается сила удара
                //getDamageForce = ;
                isUncontrollable = true;
                StartCoroutine(EndUncontrollable());
                isInvulnerable = true;
                StartCoroutine(EndInvulnerable());
            }
        }
    }

    private void Heal()
    {
        isHealing = true;
        healTimer += Time.deltaTime;
        if (healTimer >= manaLoseTime)
        {
            resourcesManager.ChangeMana(-1);
            healTimer -= manaLoseTime;
            healCounts++;
            if (healCounts == 3)
            {
                resourcesManager.ChangeHp(1);
                isHealing = false;
                healTimer = 0;
                healCounts = 0;
            }
        }
    }

    private void Attack(int attackType) //0 - вправо, 1 - влево, 2 - вверх, 3 - вниз
    {
        canJumping = false; //?

        if (attackType == 0 && playerOrientation == -1)
            attackType = 1;
        canAttack = false;
        isAttacking = true;
        StartCoroutine(Attacking());

        attackHitboxActivator.ActivateHitbox(attackType);
    }

    public void EnemyAttacked(PlayerTriggerType triggerType)
    {
        Vector2 kickbackForce;

        if (!isHitEnemy)
        {
            isHitEnemy = true;
            Invoke("ChangeIsHitEnemy", 0.1f); //?
            if (triggerType == PlayerTriggerType.Down)//весь этот kickbackForce сосал
            {
                kickbackForce = botAirAttackForce;
                rb.linearVelocityY = 0;
                canAirDash = true;
            }
            else if (triggerType == PlayerTriggerType.Right)
            {
                rb.linearVelocityX = 0;
                kickbackForce = -attackForce;
            }
            else if (triggerType == PlayerTriggerType.Left)
                kickbackForce = attackForce;
            else
                kickbackForce = Vector2.zero;
            rb.AddForce(kickbackForce);
        }
        resourcesManager.ChangeMana(1);
    }

    public void EnvironmentAttacked(PlayerTriggerType triggerType) // ?????????
    {
        Vector2 kickbackForce;

        if (!isHitEnemy)
        {
            isHitEnemy = true;
            Invoke("ChangeIsHitEnemy", 0.1f); //?
            if (triggerType == PlayerTriggerType.Down)//весь этот kickbackForce сосал
            {
                kickbackForce = botAirAttackForce;
                rb.linearVelocityY = 0;
                canAirDash = true;
            }
            else if (triggerType == PlayerTriggerType.Right)
            {
                rb.linearVelocityX = 0;
                kickbackForce = -attackForce;
            }
            else if (triggerType == PlayerTriggerType.Left)
                kickbackForce = attackForce;
            else
                kickbackForce = Vector2.zero;
            rb.AddForce(kickbackForce);
        }
    }

    private void ChangeIsHitEnemy()
    {
        isHitEnemy = false;
    }
    IEnumerator Dashing(int dashType) //имя
    {
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        if (dashType == 0)
        {
            yield return new WaitForSeconds(dashCooldown);
            canGroundDash = true;
        }
    }

    IEnumerator Attacking() //имя
    {
        yield return new WaitForSeconds(attackTime);
        isAttacking = false;
        yield return new WaitForSeconds(attackCooldown - attackTime);
        canAttack = true;
    }

    IEnumerator EndUncontrollable()
    {
        yield return new WaitForSeconds(uncontrollableTimeFromDamage);
        isUncontrollable = false;
    }

    IEnumerator EndInvulnerable()
    {
        yield return new WaitForSeconds(invulnerableTimeFromDamage);
        isInvulnerable = false;
    }
    private void ChangeState()
    {
        //либо самыми первыми dead и getDamage и return, либо в конце. 
        if (!isDashing && !isAttacking) //...    
        {
            if (GroundCheck())
            {
                canAirDash = true; //...
                if (CanInteract() && interactAction.IsPressed())
                    nextState = PlayerState.Interact;
                else if (healAction.IsPressed() && (resourcesManager.CanUseMana(3) || isHealing)) // добавить условие
                    nextState = PlayerState.Heal;
                else if (isTopAttackPressed && canAttack)
                    nextState = PlayerState.TopAttack;
                else if (isAttackPressed && canAttack)
                    nextState = PlayerState.Attack;
                else if (jumpAction.IsPressed() && canJumping)
                    nextState = PlayerState.Jump;
                else if (rb.linearVelocityX < -0.01 || rb.linearVelocityX > 0.01)
                    nextState = PlayerState.Run;
                else if (currentState != PlayerState.Interact)
                    nextState = PlayerState.Idle;
                else
                    nextState = PlayerState.Interact;
                if (dashAction.WasPressedThisFrame() && canGroundDash)
                {
                    nextState = PlayerState.Dash;
                    dashType = 0;
                }
            }
            else
            {
                if (!jumpAction.IsPressed() || currentState != PlayerState.Jump || !canJumping)
                    nextState = PlayerState.Fall;
                if (canAttack)
                {
                    if (isAirBotAttackPressed)
                        nextState = PlayerState.AirBotAttack;
                    else if (isTopAttackPressed)
                        nextState = PlayerState.AirTopAttack;
                    else if (isAttackPressed)
                        nextState = PlayerState.AirAttack;
                }
                if (dashAction.WasPressedThisFrame() && canAirDash)
                {
                    nextState = PlayerState.Dash;
                    dashType = 1;
                }
            }
        }
        //Debug.Log(currentState);



        if (isUncontrollable) //isUncontrollable??
            nextState = PlayerState.GetDamage;
        if (currentState != nextState)
        {
            isStateRepeat = false;
            previousState = currentState;
            currentState = nextState;
            if (previousState == PlayerState.Jump) // && currentState != PlayerState.Jump
                needToCancelEffects = true;
            else if (previousState == PlayerState.Heal)
            {
                healTimer = 0;
                healCounts = 0;
                isHealing = false;
            }
            else if (previousState == PlayerState.Interact && interactable != null)
            {
                interactable.ShowInput();
            }
            switch (currentState)
            {
                case PlayerState.Idle:
                    Idle();
                    break;
                case PlayerState.Run:
                    Run();
                    break;
                case PlayerState.Jump:
                    Jump();
                    break;
                case PlayerState.Fall:
                    Fall();
                    break;
                case PlayerState.Dash:
                    Dash(dashType);
                    break;
                case PlayerState.Attack:
                    Attack(0);
                    break;
                case PlayerState.TopAttack:
                    Attack(2);
                    break;
                case PlayerState.AirAttack:
                    Attack(0);
                    break;
                case PlayerState.AirBotAttack:
                    Attack(3); //5? + физика не в FixedUpdate
                    break;
                case PlayerState.AirTopAttack:
                    Attack(2);
                    break;
                case PlayerState.Heal:
                    Heal();
                    break;
                case PlayerState.GetDamage: //надо оно?
                    break;
                case PlayerState.Interact: //??
                    Interact();
                    break;
            }
        }
        else
        {
            isStateRepeat = true;
            if (currentState == PlayerState.Jump) //Это тоже что-то не то
                Jump();
            else if (currentState == PlayerState.Heal)
                Heal();
        }
        //мб это не очень хорошо
    }
    private bool GroundCheck()
    {
        return groundCheck.Check();
        //return transform.position.y < 0; //исправить 
    }
    private void CanselEffects() //мб сменить название
    {
        rb.linearVelocityY = 0; //менять эффекты в зависимости от currentState
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Interactable")) //??
        {
            interactable = collision.GetComponent<IInteractable>();
            inInteractiveZone = true;
            if(currentState != PlayerState.Interact)
                interactable.ShowInput();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            inInteractiveZone = false;
            interactable.HideInput();
        }
    }

    private void AttackPressed(InputAction.CallbackContext context) 
    {
        isAttackPressed = true;
    }

    private void AirBotAttackPressed(InputAction.CallbackContext context)
    {
        isAirBotAttackPressed = true;
    }

    private void TopAttackPressed(InputAction.CallbackContext context)
    {
        isTopAttackPressed = true;
    }

    public void SetInteractState()
    {
        currentState = PlayerState.Interact;
    }
}