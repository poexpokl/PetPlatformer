using UnityEngine;
using static PlayerController;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerController playerController;
    private Animator animator;
    private bool dieAnimationStarted;
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }
    private void LateUpdate()
    {
        if (playerController.enabled)
        {
            ResetAnimationVariables();
            if (playerController.currentState == PlayerState.Idle)
                animator.SetBool("IsIddling", true);
            else if (playerController.currentState == PlayerState.Run)
                animator.SetBool("IsRunning", true);
            else if (playerController.currentState == PlayerState.Jump)
                animator.SetBool("IsJumping", true);
            else if (playerController.currentState == PlayerState.Fall)
                animator.SetBool("IsFalling", true);
            else if (playerController.currentState == PlayerState.Dash)
                animator.SetBool("IsDashing", true);
            else if (playerController.currentState == PlayerState.Attack)
                animator.SetBool("IsGroundAttacking", true);
            else if (playerController.currentState == PlayerState.TopAttack)
                animator.SetBool("isTopGroundAttack", true);
            else if (playerController.currentState == PlayerState.AirAttack)
                animator.SetBool("IsAirAttacking", true);
            else if (playerController.currentState == PlayerState.AirBotAttack)
                animator.SetBool("IsBotAirAttacking", true);
            else if (playerController.currentState == PlayerState.AirTopAttack)
                animator.SetBool("IsTopAirAttacking", true);
            else if (playerController.currentState == PlayerState.Heal)
                animator.SetBool("IsHealing", true);
            else if (playerController.currentState == PlayerState.GetDamage)
                animator.SetBool("IsGettingDamage", true);
            else if (playerController.currentState == PlayerState.Interact) // ...........
                animator.SetBool("IsHealing", true);

            if (playerController.isStateRepeat)
                animator.SetBool("IsStateRepeat", true);
        }
        else if (!dieAnimationStarted)
        {
            ResetAnimationVariables();
            dieAnimationStarted = true;
            animator.SetBool("IsDead", true);
            animator.SetBool("IsStateRepeat", true);
            animator.Play("Die");
        }
    }

    private void ResetAnimationVariables()
    {
        animator.SetBool("IsIddling", false);
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsFalling", false);
        animator.SetBool("IsDashing", false);
        animator.SetBool("IsGroundAttacking", false);
        animator.SetBool("isTopGroundAttack", false);
        animator.SetBool("IsAirAttacking", false);
        animator.SetBool("IsBotAirAttacking", false);
        animator.SetBool("IsTopAirAttacking", false);
        animator.SetBool("IsHealing", false);
        animator.SetBool("IsGettingDamage", false);
        animator.SetBool("IsStateRepeat", false);
    }
}
