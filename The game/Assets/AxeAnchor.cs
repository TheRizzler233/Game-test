using UnityEngine;

public class AxeAnchor : MonoBehaviour
{
    [Header("References")]
    public Rigidbody playerRigidbody;
    public Transform axeHead;

    [Header("Detection")]
    public LayerMask environmentLayer;
    public float detectionRadius = 0.12f;

    [Header("Push Settings")]
    public float pushStrength = 8f;
    public float maxPlayerSpeed = 20f;

    private Vector3 previousAxePosition;

    void Start()
    {
        if (axeHead != null)
        {
            previousAxePosition = axeHead.position;
        }
    }

    void FixedUpdate()
    {
        if (playerRigidbody == null || axeHead == null)
        {
            return;
        }

        // Calculate how much the axe head moved
        // since the previous physics frame.
        Vector3 axeMovement =
            axeHead.position - previousAxePosition;

        previousAxePosition = axeHead.position;

        // Check ONLY the axe head.
        bool touchingEnvironment = IsAxeTouchingEnvironment();

        // If the axe is NOT touching anything,
        // don't affect the player at all.
        if (!touchingEnvironment)
        {
            LimitPlayerSpeed();
            return;
        }

        // Axe IS touching the environment.
        // Push the player in the opposite direction
        // from the axe head's movement.
        if (axeMovement.sqrMagnitude > 0.000001f)
        {
            Vector3 pushDirection =
                -axeMovement.normalized;

            float movementAmount =
                axeMovement.magnitude;

            Vector3 force =
                pushDirection *
                movementAmount *
                pushStrength;

            playerRigidbody.AddForce(
                force,
                ForceMode.Impulse
            );
        }

        LimitPlayerSpeed();
    }

    bool IsAxeTouchingEnvironment()
    {
        Collider[] hits = Physics.OverlapSphere(
            axeHead.position,
            detectionRadius,
            environmentLayer,
            QueryTriggerInteraction.Ignore
        );

        return hits.Length > 0;
    }

    void LimitPlayerSpeed()
    {
        if (playerRigidbody.velocity.magnitude > maxPlayerSpeed)
        {
            playerRigidbody.velocity =
                playerRigidbody.velocity.normalized *
                maxPlayerSpeed;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (axeHead == null)
        {
            return;
        }

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            axeHead.position,
            detectionRadius
        );
    }
}