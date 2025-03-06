using UnityEngine;

public class CameraFollowX : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the Inspector
    public bool smoothFollow = true;
    public float smoothSpeed = 5f;

    public bool cameraLocked = false;

    private float fixedY;
    private float fixedZ;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player not assigned to CameraFollowX script!");
            return;
        }

        // Store the initial Y and Z positions of the camera
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (player != null && !cameraLocked)
        {
            // Smoothly follow the player's X position while keeping Y and Z fixed
            Vector3 targetPosition = new Vector3(player.position.x, fixedY, fixedZ);
            if (smoothFollow){
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
            } else {
                transform.position = targetPosition;
            }            
        }
    }
}