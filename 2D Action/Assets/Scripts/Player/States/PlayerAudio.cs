using UnityEngine;
using UnityEngine.Audio;
using static PlayerController;

public class PlayerAudio : MonoBehaviour
{
    private PlayerController playerController;
    private float stepTime;
    [SerializeField] private AudioClip[] attackAudio;
    [SerializeField] private AudioClip runAudio;
    [SerializeField] private AudioClip dashAudio;
    [SerializeField] private AudioClip healAudio;
    [SerializeField] private AudioClip getDamageAudio;
    [SerializeField] private AudioClip deathAudio;
    private AudioSource audioSource;
    private bool dieSoundPlayed;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
    }
    private void LateUpdate()
    {
        if (playerController.enabled) 
        {
            if (!playerController.isStateRepeat)
            {
                if (playerController.previousState == PlayerState.Run)
                    stepTime = audioSource.time;
                audioSource.loop = false;
                audioSource.Stop();

                if (playerController.currentState == PlayerState.Dash) //switch case?
                {
                    audioSource.clip = dashAudio; //мб тут контролировать громкость или перезаписать аудиофайлы
                    audioSource.Play();
                }
                else if (playerController.currentState == PlayerState.Run)
                {
                    audioSource.clip = runAudio;
                    audioSource.loop = true;
                    audioSource.time = stepTime;
                    audioSource.Play();
                }
                else if (playerController.currentState == PlayerState.GetDamage)
                {
                    audioSource.clip = getDamageAudio;
                    audioSource.Play();
                }
                /*
                else if(currentState == PlayerState.Jump) 
                {
                    audioSource.clip = jumpAudio;
                    audioSource.Play();
                }*/
                else if (playerController.currentState == PlayerState.Heal)
                {
                    audioSource.clip = healAudio;
                    audioSource.Play();
                }
                else if (playerController.currentState == PlayerState.Attack || playerController.currentState == PlayerState.AirAttack
                    || playerController.currentState == PlayerState.TopAttack || playerController.currentState == PlayerState.AirBotAttack
                    || playerController.currentState == PlayerState.AirTopAttack)
                {
                    audioSource.clip = attackAudio[1];
                    audioSource.Play();
                    //attackIndex++;
                }
                //получение урона
            }
        }
        else if (!dieSoundPlayed)
        {
            dieSoundPlayed = true;
            audioSource.clip = deathAudio;
            audioSource.Play();
        }
    }
}
