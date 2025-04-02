using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float jumpForce = 7f;
    public float gravity = 9.81f;
    public CharacterController controller;

    private bool canDash = true;
    private bool isDashing;
    private float dashForce = 20f;
    private float dashTime = 0.2f;
    private float dashCooldown = 1f;

    [SerializeField] Rigidbody rb;

    private Vector3 moveDirection;
    private bool isGrounded;
    private Vector3 startPosition;

    public AudioClip buttonSound;
    public AudioClip playerHurt;
    public AudioClip enemyHurt;
    public AudioClip playerJump;
    public AudioClip playerDash;
    public AudioClip[] footstepSounds;  
    private AudioSource audioSource;  // AudioSource to play the sounds
    private float stepTimer = 0f;  // Timer to control when to play the next footstep sound
    public float stepInterval = 0.3f; // Interval between footstep sounds (adjust based on your movement speed)

    void Start()
    {
        controller = GetComponent<CharacterController>();
        startPosition = transform.position; // Store the starting position
        audioSource = GetComponent<AudioSource>();  // Get the AudioSource component
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }
        isGrounded = controller.isGrounded;

        float moveX = Input.GetAxis("Horizontal");
        
        Vector3 move = transform.right * moveX;
        moveDirection.x = move.x * moveSpeed;

        if((Input.GetKeyDown(KeyCode.Space)) && canDash && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "DrawnLevel")
        {
            audioSource.PlayOneShot(playerDash);
            StartCoroutine(Dash());
        }

        if (isGrounded)
        {
                    
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                audioSource.PlayOneShot(playerJump);
                moveDirection.y = jumpForce*1.1f;
            }
            else
            {
                moveDirection.y = -0.1f; // Reset vertical movement when grounded
            }
            // Handle footstep sounds when moving
            if (Mathf.Abs(moveDirection.x) > 0 && stepTimer <= 0f)
            {
                PlayFootstepSound();
                stepTimer = stepInterval;  // Reset the timer
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
            if (moveDirection.y > 0)
            {
                if (Input.GetKeyUp(KeyCode.UpArrow))
                {
                    moveDirection.y = 0;
                }
            }
            if (moveDirection.y < 0)
            {
                moveDirection.y = moveDirection.y * 1.02f;
                if (moveDirection.y < -10)
                {
                    moveDirection.y = -10;
                }
            }
            
           
            
        }

        stepTimer -= Time.deltaTime; // Decrease the step timer each frame
        controller.Move(moveDirection * Time.deltaTime);

        if(transform.position.y <= -10){
            Respawn();
        }
    }

   private void OnControllerColliderHit(ControllerColliderHit hit)
    {
    //Debug.Log("Player hit: " + hit.gameObject.name);

        if (hit.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Player hit an Enemy! Respawning...")

            // If player is in air and collides, enemy dies
            if(!controller.isGrounded)
            {
                audioSource.PlayOneShot(enemyHurt);
                hit.gameObject.SetActive(false);
            }
            else
            {
                audioSource.PlayOneShot(playerHurt);
                Respawn();
            }
        }

        if (hit.gameObject.CompareTag("Spike"))
        {
            //Debug.Log("Player hit an Enemy! Respawning...")

            // If player is in air and collides, enemy dies
           
            
                audioSource.PlayOneShot(playerHurt);
                Respawn();
            
        }

        if (hit.gameObject.CompareTag("end"))
        {
            if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "PixelLevel")
            {
                audioSource.PlayOneShot(buttonSound);
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            }
            
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "DrawnLevel")
            {
                audioSource.PlayOneShot(buttonSound);
                UnityEngine.SceneManagement.SceneManager.LoadScene("3dLevel");
            }
        }
    }

    void Respawn()
    {
        transform.position = startPosition; // Reset player to start position
        moveDirection = Vector3.zero; // Stop any movement
    }

    private IEnumerator Dash()
    {
        controller.enabled = false;
        canDash = false;
        isDashing = true;
        rb.linearVelocity = new Vector3(transform.localScale.x * dashForce, 0f, 0f);
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        controller.enabled = true;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        
    }

    private void PlayFootstepSound()
    {
        // Play a random footstep sound from the array
        if (footstepSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, footstepSounds.Length);
            audioSource.PlayOneShot(footstepSounds[randomIndex]);
        }
    }

    
}