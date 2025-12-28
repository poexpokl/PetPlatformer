using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] string isAttack;
    [SerializeField] string isRun;
    [SerializeField] string isWalk;
    [SerializeField] string isStay;
    [SerializeField] string isTeleport;
    [SerializeField] string isDead;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AttackAnimation(bool b)
    {
        animator.SetBool(isAttack, b);
    }

    public void RunAnimation(bool b)
    {
        animator.SetBool(isRun, b);
    }

    public void WalkAnimation(bool b)
    {
        animator.SetBool(isWalk, b);
    }

    public void StayAnimation(bool b)
    {
        animator.SetBool(isStay, b);
    }
    public void TeleportAnimation(bool b)
    {
        animator.SetBool(isTeleport, b);
    }
    public void DeadAnimation()
    {
        animator.SetBool(isDead, true);
    }
}
