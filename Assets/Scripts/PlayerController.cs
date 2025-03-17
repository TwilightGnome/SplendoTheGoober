using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float gravity = 9.81f;
    public CharacterController controller;

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
        isGrounded = controller.isGrounded;

        float moveX = Input.GetAxis("Horizontal");
        
        Vector3 move = transform.right * moveX;
        moveDirection.x = move.x * moveSpeed;

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
                if (moveDirection.y < -12)
                {
                    moveDirection.y = -12;
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
        //Debug.Log("Player hit an Enemy! Respawning...");
        Respawn();
    }
}

    void Respawn()
    {
        transform.position = startPosition; // Reset player to start position
        moveDirection = Vector3.zero; // Stop any movement
    }
}