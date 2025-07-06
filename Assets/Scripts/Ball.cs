using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private bool isPossessed = false;
    private Transform followTarget;

    public float stopDrag = 1f;
    public float possessionDrag = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!isPossessed)
        {
            rb.drag = stopDrag;
        }
        else if (followTarget != null)
        {
            rb.drag = possessionDrag;

            Vector3 targetPosition = followTarget.position;
            Vector3 currentPosition = transform.position;

            Vector3 direction = new Vector3(
                targetPosition.x - currentPosition.x,
                0f,
                targetPosition.z - currentPosition.z
            );

            rb.velocity = new Vector3(
                direction.x / Time.fixedDeltaTime,
                rb.velocity.y, 
                direction.z / Time.fixedDeltaTime
            );

        }
    }

    public void Possess(Transform player, Transform holdPoint)
    {
        isPossessed = true;
        followTarget = holdPoint;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void DetachFromPlayer()
    {
        isPossessed = false;
        followTarget = null;
    }
}
