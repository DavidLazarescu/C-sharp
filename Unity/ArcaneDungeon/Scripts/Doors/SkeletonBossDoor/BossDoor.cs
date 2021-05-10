using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    //bools
    [HideInInspector] public bool isOpened = false;

    //floats
    [SerializeField] private float cooldown = 3.0f;
    private float currentCooldown = 0.0f;



    //Audiomanager
    private AudioManager audioManager;

    //Animators
    private Animator animator;

    //Gameobjects
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject lockedText;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
    }

    public void bossDoorMove()
    {
        if (!isOpened && currentCooldown <= 0.0f)
        {
            animator.ResetTrigger("BossDoor_Open_Animation");
            animator.SetTrigger("BossDoor_Open_Animation");
            isOpened = true;

            boss.GetComponent<EnemySkeletonBossMovement>().enabled = true;

            //Cooldown
            currentCooldown = cooldown;
            Invoke(nameof(automaticClose), cooldown);
        }
        else if (isOpened && currentCooldown <= 0.0f)
        {
            audioManager.playSound("Door_Locked", audioManager.environment);
            lockedText.SetActive(true);
            Invoke(nameof(disableLockedText), 0.2f);
        }

        #region for normal door
        /*else if (isOpened && currentCooldown <= 0.0f)
        {
            animator.ResetTrigger("BossDoor_Close_Animation");
            animator.SetTrigger("BossDoor_Close_Animation");
            isOpened = !isOpened;

            //Cooldown
            currentCooldown = cooldown;
            Invoke(nameof(doorUseCooldown), cooldown);
        }*/
        #endregion
    }


    private void disableLockedText()
    {
        lockedText.SetActive(false);
    }

    private void automaticClose()
    {
        animator.ResetTrigger("BossDoor_Close_Animation");
        animator.SetTrigger("BossDoor_Close_Animation");

        //Cooldown
        currentCooldown = cooldown;
        Invoke(nameof(doorUseCooldown), cooldown);
        Invoke(nameof(afterClose), 0.25f);
    }

    private void afterClose()
    {
        audioManager.playSound("Door_Close", audioManager.environment);
    }

    private void doorUseCooldown()
    {
        currentCooldown = 0;
    }
}
