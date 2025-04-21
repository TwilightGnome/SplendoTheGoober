using UnityEngine;


public class PlayerController3D : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody theRB;
    public float jumpForce;
    private Vector3 startPosition;
    public float dashForce;
    public Animator anim;
    private AudioSource audioSource;  // AudioSource to play the sounds
    public AudioClip buttonSound;
    public AudioClip jumpSound;
    public AudioClip[] stepSounds;
    public AudioClip hurtSound;

    private bool wasGrounded;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
        wasGrounded = IsGrounded();
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
            if (jumpSound != null)
                audioSource.PlayOneShot(jumpSound);
            
        }

        theRB.linearVelocity = velocity; 

        if(theRB.transform.position.y < -10)
        {
            PlayHurtSound();
            Respawn();
        }

        bool isGrounded = IsGrounded();

        // Landing detection for footstep sounds
        if (!wasGrounded && isGrounded)
        {
            PlayStepSound();
        }

        wasGrounded = isGrounded;

        if (!IsGrounded())
        {
            GetComponentInChildren<Animator>().SetBool("isjumping", true);
            
        }

        if (IsGrounded())
        {
            GetComponentInChildren<Animator>().SetBool("isjumping", false);
        }

        if (theRB.linearVelocity == Vector3.zero)
        {
            GetComponentInChildren<Animator>().SetBool("isrunning", false);
        }

        if (theRB.linearVelocity != Vector3.zero)
        {
            GetComponentInChildren<Animator>().SetBool("isrunning", true);
        }
      

        GetComponentInChildren<Transform>().rotation = Quaternion.LookRotation(transform.forward, Vector3.up);

        if(theRB.transform.position.z > 508)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Cut_Scene_3");
        }
        

    }
    

    //private void OrollerColliderHit(ControllerColliderHit hit)
    //{
    //    if (hit.gameObject.CompareTag("end"))
     //   {   
            
                //audioSource.PlayOneShot(buttonSound);
    //            UnityEngine.SceneManagement.SceneManager.LoadScene("Cut_Scene_3");
            
    //    }
    //}

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

    void PlayStepSound()
    {
        if (stepSounds.Length > 0)
        {
            int index = Random.Range(0, stepSounds.Length);
            audioSource.PlayOneShot(stepSounds[index]);
        }
    }

    void PlayHurtSound()
    {
        if (hurtSound != null)
            audioSource.PlayOneShot(hurtSound);
    }

}
