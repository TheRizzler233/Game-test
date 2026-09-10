using UnityEngine;

public class AxeController : MonoBehaviour
{
    public Transform player;
    public Camera cam;

    public float rotationSpeed = 20f;

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    void Update()
    {
        if (player == null || cam == null)
        {
            return;
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        // A plane facing the camera, centered on the player.
        Plane plane = new Plane(
            cam.transform.forward,
            player.position
        );

        float distance;

        if (!plane.Raycast(ray, out distance))
        {
            return;
        }

        Vector3 mousePosition = ray.GetPoint(distance);

        Vector3 direction =
            mousePosition - player.position;

        if (direction.sqrMagnitude < 0.001f)
        {
            return;
        }

        direction.Normalize();

        // Point the pivot's FORWARD (+Z) toward the mouse.
        Quaternion targetRotation =
            Quaternion.LookRotation(
                direction,
                Vector3.up
            );

        transform.rotation =
            Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
    }
}