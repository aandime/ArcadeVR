using UnityEngine;

public class DartStick : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasHit = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return; // Ignore further collisions after the first hit

        if (collision.gameObject.CompareTag("Dartboard")) // Make sure to tag your dartboard with "Dartboard"
        {
            hasHit = true;
            StickDart(collision);
        }
    }

    private void StickDart(Collision collision)
    {
        rb.isKinematic = true; // Stop physics simulation for the dart
        transform.SetParent(collision.transform); // Make the dart a child of the dartboard to stick it
    }
}
