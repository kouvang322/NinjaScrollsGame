using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    public float rotationSpeed;

    [SerializeField]
    public float jumpHeight;

    [SerializeField] 
    public float gravityMultiplier;

    [SerializeField] 
    public float jumpHorizontalSpeed;

    [SerializeField]
    public float jumpButtonGracePeriod;

    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private HealthBarUI healthBar;

    private Animator animator;
    private CharacterController characterController;
    private BoxCollider attackHitBoxCollider;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool isJumping;
    private bool isGrounded;
    public float Health, MaxHealth;
    public GameObject GameOverCanvas;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        attackHitBoxCollider = GetComponent<BoxCollider>();
        originalStepOffset = characterController.stepOffset;
        healthBar.SetMaxHealth(MaxHealth);
        animator.SetBool("isAlive", true);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);


        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            inputMagnitude /= 2;
        }

        animator.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);

        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();


        float gravity = Physics.gravity.y * gravityMultiplier;


        if(isJumping && ySpeed > 0 && Input.GetButton("Jump") == false)
        {
            gravity *= 2;
        }

        ySpeed += gravity * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -1f;
            animator.SetBool("isGrounded", true);
            isGrounded = true;
            animator.SetBool("isJumping", false);
            isJumping = false;
            animator.SetBool("isFalling", false);

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = Mathf.Sqrt(jumpHeight * -3 * gravity);
                animator.SetBool("isJumping", true);
                isJumping = true;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
            animator.SetBool("isGrounded", false);
            isGrounded = false;

            if ((isJumping && ySpeed < 0) || ySpeed < -2)
            {
                animator.SetBool("isFalling", true);
            } 
        }


        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (isGrounded == false)
        {
            Vector3 velocity = movementDirection * inputMagnitude * jumpHorizontalSpeed;
            velocity.y = ySpeed;

            characterController.Move(velocity * Time.deltaTime);
        }

        IEnumerator WaitForAnimationEnd(float sec)
        {
            yield return new WaitForSeconds(sec);
            animator.SetBool("isAttacking", false);
            attackHitBoxCollider.enabled = false;
            //Debug.Log("character atk box disabled");

        }

        if (isGrounded == true && Input.GetKeyDown(KeyCode.K))
        {
            animator.SetBool("isAttacking", true);
            attackHitBoxCollider.enabled = true;
            //Debug.Log("character atk box enabled");

            StartCoroutine(WaitForAnimationEnd(.5f));

        }


    }

    public void SetHealth(float healthChange)
    {
        Health += healthChange;
        Health = Mathf.Clamp(Health, 0, MaxHealth);

        healthBar.SetHealth(Health);
        

    }

    IEnumerator WaitForHurtAnimationEnd(float sec)
    {
        yield return new WaitForSeconds(sec);
        animator.SetBool("isHit", false);

        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Enemy hitbox

        if (other == GameObject.FindGameObjectWithTag("Enemy").GetComponent<BoxCollider>())
        {
            Debug.Log("Enemy attack hit player");

            //play player hurt animation

            animator.SetBool("isHit", true);

            SetHealth(-15f);

            // when health is 0, play death animation
            if (Health == 0)
            {
                animator.SetBool("isAlive", false);
                Debug.Log("player is dead");
                GameOverCanvas.SetActive(true);
                Cursor.lockState = CursorLockMode.None;

            }

            StartCoroutine(WaitForHurtAnimationEnd(.5f));

            // if player health is zero
            // player player death animation



        }
    }

    private void OnAnimatorMove()
    {
        if (isGrounded)
        {
            Vector3 velocity = animator.deltaPosition;
            velocity.y = ySpeed * Time.deltaTime;

            characterController.Move(velocity);

        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
