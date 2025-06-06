using UnityEngine;

public class FaceTracker : MonoBehaviour
{
    [Header("Target (player head)")]
    [Tooltip("Leave null to auto‑find the MainCamera at runtime.")]
    public Transform playerHead;

    [Header("Rotation")]
    [Tooltip("Degrees per second. Use a large value (for example 720) for instant turn.")]
    public float turnSpeed = 360f;

    [Header("Model correction")]
    [Tooltip("Add or subtract degrees so the mask truly faces forward.  " +
            "Positive = rotate clockwise when looking from above.")]
    public float yawOffset = 0f;

    private void Start()
    {
        // Auto‑detect the VR headset (camera) if not assigned
        if (playerHead == null)
        {
            GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
            if (camObj != null)
            {
                playerHead = camObj.transform;
            }
            else
            {
                Debug.LogWarning("[TikiFacePlayer] No object tagged MainCamera found in scene.");
            }
        }
    }

    private void LateUpdate()
    {
        if (playerHead == null) return;

        // Direction from Tiki to camera, ignoring vertical difference
        Vector3 direction = playerHead.position - transform.position;
        direction.y = 0f; // keep only horizontal direction

        if (direction.sqrMagnitude < 0.001f) return; // avoid zero‑length vector

        // Desired rotation
        Quaternion targetRot = Quaternion.LookRotation(direction.normalized, Vector3.up);

        // Apply yaw offset correction
        targetRot *= Quaternion.Euler(0f, yawOffset, 0f);

        // Smoothly rotate toward the target
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRot,
            turnSpeed * Time.deltaTime);
    }
}
