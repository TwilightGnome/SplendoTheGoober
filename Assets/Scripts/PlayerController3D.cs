using UnityEngine;


public class PlayerController3D : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody theRB;
    public float jumpForce;
    private Vector3 startPosition;
    public float dashForce;
    Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

            Vector3 velocity = theRB.linearVelocity;
        velocity.x = Input.GetAxis("Horizontal") * moveSpeed;
        velocity.z = Input.GetAxis("Vertical") * moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            
            velocity.y = jumpForce;
            //anim.SetBool("isjumping", true);
        }

        theRB.linearVelocity = velocity; 

        if(theRB.transform.position.y < -10)
        {
            Respawn();
        }
    }

    private void OrollerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("end"))
        {   
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "3dLevel")
            {
                //audioSource.PlayOneShot(buttonSound);
                UnityEngine.SceneManagement.SceneManager.LoadScene("Cut_Scene_3");
            }
        }
    }

    bool IsGrounded()
    {
        float GroundedDistance = 2f;

        if (theRB.linearVelocity.y == 0)
        {
            return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, GroundedDistance);
        }
        else return false;
    }

    void Respawn()
    {
        transform.position = startPosition; // Reset player to start position
        theRB.linearVelocity = Vector3.zero; // Stop any movement
    }

}
