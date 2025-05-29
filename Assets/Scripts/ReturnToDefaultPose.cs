using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class ReturnToDefaultPose : MonoBehaviour
{
    [Header("Default Return Settings")]
    [Tooltip("Time in seconds to wait before returning.")]
    public float delayBeforeReturn = 2.0f;

    [Tooltip("Speed of the return movement.")]
    public float returnSpeed = 2.0f;

    private XRGrabInteractable grab;
    private Rigidbody rb;
    private Coroutine returnRoutine;

    // Cached starting pose
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
        grab.selectExited.AddListener(OnReleased);

        // Cache the object's starting world position and rotation
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        // Cancel existing return logic, if any
        if (returnRoutine != null)
        {
            StopCoroutine(returnRoutine);
        }
        // Reset filter settings when dropped
        FilterControl.colorAdjust.active = false;
        FilterControl.depthOfField.active = false;

        returnRoutine = StartCoroutine(ReturnAfterDelay());
    }

    private IEnumerator ReturnAfterDelay()
    {
        // Wait with normal physics enabled
        yield return new WaitForSeconds(delayBeforeReturn);

        // Disable physics to begin smooth movement
        rb.isKinematic = true;

        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        float duration = Vector3.Distance(startPos, initialPosition) / returnSpeed;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            // Interpolate horizontal position
            Vector3 flatLerp = Vector3.Lerp(startPos, initialPosition, t);

            // Vertical overshoot: go 1 unit above the target in the middle of the animation
            float arcHeight = 1f; // adjust this to control overshoot
            float yOvershoot = Mathf.Lerp(startPos.y, initialPosition.y, t) + Mathf.Sin(t * Mathf.PI) * arcHeight;

            Vector3 newPos = new Vector3(flatLerp.x, yOvershoot, flatLerp.z);
            Quaternion newRot = Quaternion.Slerp(startRot, initialRotation, t);

            transform.SetPositionAndRotation(newPos, newRot);
            yield return null;
        }

        // Snap to final position
        transform.SetPositionAndRotation(initialPosition, initialRotation);
        yield return new WaitForSeconds(0.05f);
        rb.isKinematic = false;
        returnRoutine = null;
    }

    private void OnDestroy()
    {
        grab.selectExited.RemoveListener(OnReleased);
    }
}
