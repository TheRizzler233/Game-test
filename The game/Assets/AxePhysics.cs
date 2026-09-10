
using UnityEngine;

public class AxePhysics : MonoBehaviour
{
    [Header("References")]
    public Rigidbody playerRigidbody;
    public Transform axePivot;

    [Header("Movement")]
    public float pushForce = 12f;
    public float maxVelocity = 20f;

    private Vector3 previousPosition;

    private void Start()
    {
        previousPosition = transform.position;
    }

    private void FixedUpdate()
    {
        previousPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 axeVelocity =
            (transform.position - previousPosition) /
            Time.fixedDeltaTime;

        if (axeVelocity.sqrMagnitude < 0.01f)
            return;

        Vector3 pushDirection = -axeVelocity.normalized;

        playerRigidbody.AddForce(
            pushDirection * pushForce,
            ForceMode.Impulse
        );

        LimitVelocity();
    }

    private void LimitVelocity()
    {
        if (playerRigidbody.velocity.magnitude > maxVelocity)
        {
            playerRigidbody.velocity =
                playerRigidbody.velocity.normalized *
                maxVelocity;
        }
    }
}

