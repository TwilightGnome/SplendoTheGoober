using UnityEngine;

public class PlayerSpriteAnimator : MonoBehaviour
{
    [Header("References")]

    public bool backwards = false;
    public Transform spriteTransform;         // Reference to the child GameObject with the SpriteRenderer
    public Animator spriteAnimator;           // Animator on the child object

    [Header("Settings")]
    public float animationSpeedMultiplier = 0.5f;  // Adjust to slow down or speed up animation

    public float scale = 3.0f;

    private float moveInput;

    void Start()
    {
        if (spriteAnimator != null)
        {
            spriteAnimator.speed = animationSpeedMultiplier;
        }
    }

    void Update()
    {
        spriteAnimator.SetFloat("Speed", Mathf.Abs(moveInput));
        // Example: Using horizontal input — replace with your actual movement logic if needed
        moveInput = Input.GetAxisRaw("Horizontal");

        // 1. Switch animation state (Idle ↔ Walking) based on movement
        spriteAnimator.SetFloat("Speed", Mathf.Abs(moveInput));

        int b = 1; 
        if (backwards){
            b = -1;
        }

        // 2. Flip sprite direction
        if (moveInput > 0)
        {
            spriteTransform.localScale = new Vector3(-scale*b, scale, 1);
        }
        else if (moveInput < 0)
        {
            spriteTransform.localScale = new Vector3(scale*b, scale, 1);
        }
    }
}