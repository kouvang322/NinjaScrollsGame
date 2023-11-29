using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private CapsuleCollider enemyBodyCollider;
    private BoxCollider enemyAttackBoxCollider;
    private GameObject enemyFOV;
    private Animator enemyAnimator;
    private int enemyHealth = 2;

    SplineFollower SplineFollower;

    // Start is called before the first frame update
    void Start()
    {
        enemyBodyCollider = GetComponent<CapsuleCollider>();
        enemyAttackBoxCollider = GetComponent<BoxCollider>();
        enemyAnimator = GetComponent<Animator>();
        enemyAnimator.SetBool("isAlive", true);
        enemyAnimator.SetBool("isMoving", true);
        SplineFollower = GetComponent<SplineFollower>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //player's attack box hits enemy
    private void OnTriggerEnter(Collider other)
    {
        PlayerKills playerKills = other.GetComponent<PlayerKills>(); 

        // players sword
        if (other == GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>() || playerKills != null)
        {
            Debug.Log("Player attack hit with enemy");

            //play enemy hurt animation

            enemyAnimator.SetBool("isHit", true);
            enemyAnimator.SetBool("isMoving", false);
            SplineFollower.enabled = false;


            // enemy health goes down
            enemyHealth--;

            Debug.Log(enemyHealth.ToString());

            // enemy death animation plays
            if (enemyHealth == 0)
            {
                enemyAnimator.SetBool("isAlive", false);
                playerKills.EnemyKilled();

            }

            StartCoroutine(WaitForHurtAnimationEnd(.5f));

            //if enemy health equals zero, enemy dies
            //play death animation for enemy


            //enemy disappears after animation
            //gameObject.SetActive(false);

        }


        if (other == GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>())
        {
            enemyAnimator.SetBool("isAttacking", true);
            enemyAnimator.SetBool("isMoving", false);
            enemyAttackBoxCollider.enabled = true;
            SplineFollower.enabled = false;

            StartCoroutine(WaitForAnimationEnd(.5f));

        }

    }

    IEnumerator WaitForAnimationEnd(float sec)
    {
        yield return new WaitForSeconds(sec);
        enemyAnimator.SetBool("isAttacking", false);
        enemyAttackBoxCollider.enabled = false;

        
    }


    IEnumerator WaitForHurtAnimationEnd(float sec)
    {
        yield return new WaitForSeconds(sec);
        enemyAnimator.SetBool("isHit", false);

    }

}
