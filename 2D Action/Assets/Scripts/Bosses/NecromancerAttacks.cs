using System;
using System.Collections;
using UnityEngine;

public class NecromancerAttacks : MonoBehaviour
{
    [SerializeField] private GameObject[] lasers;
    [SerializeField] private GameObject sceleton;
    [SerializeField] Vector3 sceletonPosition;
    [SerializeField] private float laserAttackAnimationTime;
    [SerializeField] private float scullAttackAnimationTime;
    [SerializeField] private float AbsoluteAttackAnimationTime;
    [SerializeField] private float laserEnableTimeDivider;
    private float laserEnableTime;
    private float scullAttackTime;
    public event Action Attaked;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        lasers[0].SetActive(true);
        laserEnableTime = lasers[0].GetComponent<NecromancerLaser>().AnimationTime / laserEnableTimeDivider; // /? 
        lasers[0].SetActive(false);
        lasers[8].SetActive(true);
        scullAttackTime = lasers[8].GetComponent<NecromancerLaser>().AnimationTime;
        lasers[8].SetActive(false);
    }

    private void ActivateLaser(int laserNumber) //lasers è numbers[]
    {
        lasers[laserNumber].SetActive(true);
    }

    public void VerticalInsideLaser()
    {
        StartCoroutine(VerticalInsideLaserAttack());
    }

    IEnumerator VerticalInsideLaserAttack()
    {
        animator.SetBool("isLaserAttacking", true);
        yield return new WaitForSeconds(laserAttackAnimationTime);
        animator.SetBool("isLaserAttacking", false);
        ActivateLaser(0);
        ActivateLaser(5);
        yield return new WaitForSeconds(laserEnableTime);
        ActivateLaser(1);
        ActivateLaser(4);
        yield return new WaitForSeconds(laserEnableTime);
        ActivateLaser(2);
        ActivateLaser(3);
        yield return new WaitForSeconds(laserEnableTime * laserEnableTimeDivider);
        Attaked?.Invoke();
    }
    public void VerticalOutsideLaser()
    {
        StartCoroutine(VerticalOutsideLaserAttack());
    }

    IEnumerator VerticalOutsideLaserAttack()
    {
        animator.SetBool("isLaserAttacking", true);
        yield return new WaitForSeconds(laserAttackAnimationTime);
        animator.SetBool("isLaserAttacking", false);
        ActivateLaser(2);
        ActivateLaser(3);
        yield return new WaitForSeconds(laserEnableTime);
        ActivateLaser(1);
        ActivateLaser(4);
        yield return new WaitForSeconds(laserEnableTime);
        ActivateLaser(0);
        ActivateLaser(5);
        yield return new WaitForSeconds(laserEnableTime * laserEnableTimeDivider);
        Attaked?.Invoke();
    }

    public void VerticalChessLaser()
    {
        StartCoroutine(VerticalChessLaserAttack());
    }

    IEnumerator VerticalChessLaserAttack()
    {
        animator.SetBool("isLaserAttacking", true);
        yield return new WaitForSeconds(laserAttackAnimationTime);
        animator.SetBool("isLaserAttacking", false);
        ActivateLaser(0);
        ActivateLaser(2);
        ActivateLaser(4);
        yield return new WaitForSeconds(laserEnableTime);
        ActivateLaser(1);
        ActivateLaser(3);
        ActivateLaser(5);
        yield return new WaitForSeconds(laserEnableTime * laserEnableTimeDivider);
        Attaked?.Invoke();
    }

    public void HorizontalUpLaser()
    {
        StartCoroutine(HorizontalUpLaserAttack());
    }

    IEnumerator HorizontalUpLaserAttack()
    {
        animator.SetBool("isLaserAttacking", true);
        yield return new WaitForSeconds(laserAttackAnimationTime);
        animator.SetBool("isLaserAttacking", false);
        ActivateLaser(6);
        yield return new WaitForSeconds(laserEnableTime);
        ActivateLaser(7);
        yield return new WaitForSeconds(laserEnableTime * laserEnableTimeDivider);
        Attaked?.Invoke();
    }
    public void HorizontalDownLaser()
    {
        StartCoroutine(HorizontalDownLaserAttack());
    }

    IEnumerator HorizontalDownLaserAttack()
    {
        animator.SetBool("isLaserAttacking", true);
        yield return new WaitForSeconds(laserAttackAnimationTime);
        animator.SetBool("isLaserAttacking", false);
        ActivateLaser(7);
        yield return new WaitForSeconds(laserEnableTime);
        ActivateLaser(6);
        yield return new WaitForSeconds(laserEnableTime * laserEnableTimeDivider);
        Attaked?.Invoke();
    }
    public void SkullAttack()
    {
        StartCoroutine(SkullAttacking());
    }

    IEnumerator SkullAttacking()
    {
        animator.SetBool("isScullAttacking", true);
        yield return new WaitForSeconds(scullAttackAnimationTime);
        animator.SetBool("isScullAttacking", false);
        ActivateLaser(8);
        GameObject currentSceleton = Instantiate(sceleton, sceletonPosition, Quaternion.identity);
        currentSceleton.GetComponent<EnemyOrientation>().ChangeOrientation(); //?
        yield return new WaitForSeconds(scullAttackTime);
        Attaked?.Invoke();
    }

    public void AbsoluteLaser(int laserNumber)
    {
        StartCoroutine(AbsoluteLaserAttack(laserNumber));
    }
    public void AbsoluteLaser(int laserNumber1, int laserNumber2)
    {
        StartCoroutine(AbsoluteLaserAttack(laserNumber1, laserNumber2));
    }
    IEnumerator AbsoluteLaserAttack(int laserNumber)
    {
        animator.SetBool("isLaserAttacking", true);
        yield return new WaitForSeconds(AbsoluteAttackAnimationTime);
        animator.SetBool("isLaserAttacking", false);
        for (int i = 0; i < 6; i++)
        { 
            if(i != laserNumber)
                ActivateLaser(i);
        }
        yield return new WaitForSeconds(laserEnableTime * laserEnableTimeDivider);
        Attaked?.Invoke();
    }

    IEnumerator AbsoluteLaserAttack(int laserNumber1, int laserNumber2)
    {
        animator.SetBool("isLaserAttacking", true);
        yield return new WaitForSeconds(AbsoluteAttackAnimationTime);
        animator.SetBool("isLaserAttacking", false);
        for (int i = 0; i < 8; i++)
        {
            if(i != laserNumber1 && i != laserNumber2)
                ActivateLaser(i);
        }
        yield return new WaitForSeconds(laserEnableTime * laserEnableTimeDivider);
        Attaked?.Invoke();
    }

    public void ChangeLasersAnimationSpeed(float newSpeed)
    {
        for (int i = 0; i < lasers.Length; i++) 
        {
            lasers[i].GetComponent<NecromancerLaser>().ChangeAnimationSpeed(newSpeed);
        }
    }
}
