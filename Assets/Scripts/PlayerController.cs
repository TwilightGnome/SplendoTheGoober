using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float gravity = 9.81f;
    public CharacterController controller;
    public BoxCollider boxCollider;

    private Vector3 moveDirection;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        float moveX = Input.GetAxis("Horizontal");
        
        Vector3 move = transform.right * moveX;
        moveDirection.x = move.x * moveSpeed;

        Debug.Log(isGrounded);

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                moveDirection.y = jumpForce;
            }
            else
            {
                moveDirection.y = -0.1f; // Reset vertical movement when grounded
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        controller.Move(moveDirection * Time.deltaTime);
    }
}
