using UnityEngine;

public class GlideThingy : MonoBehaviour
{
    public Rigidbody playerRigidbody;

    [Header("Ground Friction")]
    public float groundDrag = 8f;

    [Header("Air")]
    public float airDrag = 0f;

    private bool grounded = false;

    void Start()
    {
        if (playerRigidbody == null)
        {
            playerRigidbody = GetComponent<Rigidbody>();
        }
    }

    void FixedUpdate()
    {
        if (playerRigidbody == null)
        {
            return;
        }

        if (grounded)
        {
            playerRigidbody.drag = groundDrag;
        }
        else
        {
            playerRigidbody.drag = airDrag;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer ==
            LayerMask.NameToLayer("Environment"))
        {
            grounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer ==
            LayerMask.NameToLayer("Environment"))
        {
            grounded = false;
        }
    }
}