using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public enum EnemyState
{
    Wander,
    Move,
    Attack,
    Stay,
    Dead,
    Teleport,
    GotDamage
    //и проч.
};

public abstract class EnemyStateMachine : MonoBehaviour
{

    [SerializeField] protected EnemyState state;
    protected EnemyOrientation orientation;
    protected EnemyAnimation eAnimation;
    protected bool isDied;
    protected virtual void Start()
    {
        orientation = GetComponent<EnemyOrientation>();
        eAnimation = GetComponent<EnemyAnimation>();
    }
    protected void ChangeState(EnemyState newState)
    {
        ExitState(state);
        state = newState;
        EnterState(state);
    }

    protected virtual void OnEnable()
    {
        GetComponent<EnemyHPManager>().Died += Died;
    }

    protected virtual void OnDisable()
    {
        GetComponent<EnemyHPManager>().Died -= Died;
    }
    protected void Died() 
    {
        isDied = true;
    }
    protected abstract void EnterState(EnemyState state);

    protected abstract void ExitState(EnemyState state);

}
