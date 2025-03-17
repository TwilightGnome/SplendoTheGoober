using UnityEngine;

public class enemyAniController : MonoBehaviour
{
    public Animator spriteAnimator;    
    public float animationSpeedMultiplier = 0.3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (spriteAnimator != null)
        {
            spriteAnimator.speed = animationSpeedMultiplier;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
