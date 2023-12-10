using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DartShooter : MonoBehaviour
{
    public float shootForce = 500f; // Adjust the force as needed
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Get the XR Grab Interactable component and subscribe to the selectExited event
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Apply a forward force to the dart when it's released
        rb.isKinematic = false;
        rb.AddForce(transform.forward * shootForce);
    }
}
