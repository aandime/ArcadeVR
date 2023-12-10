using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MovingDartController : MonoBehaviour
{
    public float shootForce = 500f; // Adjust the force as needed
    public Transform dartboardCenter; // Assign the center of the dartboard in the inspector
    public GameObject dartPrefab; // Reference to the Dart prefab

    private Rigidbody rb;
    private bool hasHit = false;
    private bool canScore = false; // Flag to check if scoring is allowed
    public float scoreDelay = 2f; // Delay in seconds before scoring is enabled

    private Vector3 initialPosition; // To store the initial position for spawning new darts
    private Quaternion initialRotation; // To store the initial rotation for spawning new darts

    // Define your scoring radii thresholds
    public float centerZoneRadius = 0.05f;
    public float mediumZoneRadius = 0.17f;
    public float outerZoneRadius = 0.287f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnRelease);
            grabInteractable.selectEntered.AddListener(OnGrab);
        }

        initialPosition = transform.position; // Save the initial position
        initialRotation = transform.rotation; // Save the initial rotation
    }

    private void Start()
    {
        // Start a delayed function to enable scoring
        Invoke(nameof(EnableScoring), scoreDelay);
    }

    private void EnableScoring()
    {
        canScore = true;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (hasHit) return;

        rb.isKinematic = false;
        rb.AddForce(transform.forward * shootForce);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        hasHit = false;
        rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit || !canScore) return;

        if (collision.gameObject.CompareTag("Dartboard"))
        {
            hasHit = true;
            CalculateScore(collision);
            StickDart(collision);
            SpawnNewDart(); // Spawn a new dart
        }
    }

    private void CalculateScore(Collision collision)
    {
        float distance = Vector3.Distance(collision.contacts[0].point, dartboardCenter.position);

        if (distance <= centerZoneRadius)
        {
            MovingDartboardScoring.Instance.AddScore(10);
        }
        else if (distance <= mediumZoneRadius)
        {
            MovingDartboardScoring.Instance.AddScore(5);
        }
        else if (distance <= outerZoneRadius)
        {
            MovingDartboardScoring.Instance.AddScore(1);
        }
    }

    private void StickDart(Collision collision)
    {
        rb.isKinematic = true;
        transform.SetParent(collision.transform);
    }

    private void SpawnNewDart()
    {
        if (dartPrefab != null)
        {
            Instantiate(dartPrefab, initialPosition, initialRotation);
        }
    }

    private void OnDestroy()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.RemoveListener(OnRelease);
            grabInteractable.selectEntered.RemoveListener(OnGrab);
        }
    }
}
