using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //IEnumerator WaitForAnimationEnd(float sec, Animator enemyAnimator, BoxCollider enemyAttackBoxCollider)
    //{
    //    yield return new WaitForSeconds(sec);
    //    enemyAnimator.SetBool("isAttacking", false);
    //    enemyAttackBoxCollider.enabled = false;
    //}

    //// player enters enemy view, enemy attacks
    //private void OnTriggerEnter(Collider other)
    //{
    //    //Debug.Log(other);

    //    if (other == GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>())
    //    {
    //        Debug.Log("Player entered vision");

    //        //enemyAnimator.SetBool("isAttacking", true);
    //        //enemyAttackBoxCollider.enabled = true;

    //        //StartCoroutine(WaitForAnimationEnd(1f));
    //    }

    //}
}
