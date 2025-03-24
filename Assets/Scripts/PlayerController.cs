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

    void Start()
    {
        controller = GetComponent<CharacterController>();
        startPosition = transform.position; // Store the starting position
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

        if((Input.GetKeyDown(KeyCode.Space)) && canDash)
        {
            StartCoroutine(Dash());
        }

        if (isGrounded)
        {
                    
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                moveDirection.y = jumpForce*1.1f;
            }
            else
            {
                moveDirection.y = -0.1f; // Reset vertical movement when grounded
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
                hit.gameObject.SetActive(false);
            }
            else
            {
                Respawn();
            }
        }

        if (hit.gameObject.CompareTag("Spike"))
        {
            //Debug.Log("Player hit an Enemy! Respawning...")

            // If player is in air and collides, enemy dies
           
            
                Respawn();
            
        }

        if (hit.gameObject.CompareTag("end"))
        {
            
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            
        }
    }

    void Respawn()
    {
        transform.position = startPosition; // Reset player to start position
        moveDirection = Vector3.zero; // Stop any movement
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.linearVelocity = new Vector2(transform.localScale.z * dashForce, 0f);
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    
}