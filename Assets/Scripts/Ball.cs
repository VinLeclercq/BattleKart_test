using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private bool isPossessed = false;
    private Transform followTarget;

    [Tooltip("Frottement lorsque la balle est libre")]
    public float stopDrag = 1f;

    [Tooltip("Frottement lorsque la balle est possédée")]
    public float possessionDrag = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private bool recentlyDetached = false;

    void FixedUpdate()
    {
        if (recentlyDetached)
        {
            recentlyDetached = false;
            return;
        }

        if (isPossessed && followTarget != null)
        {
            rb.drag = possessionDrag;

            Vector3 targetPos = followTarget.position;
            Vector3 pos = transform.position;

            Vector3 direction = new Vector3(
                targetPos.x - pos.x,
                0f,
                targetPos.z - pos.z
            );

            rb.velocity = direction / Time.fixedDeltaTime;
        }
        else
        {
            rb.drag = stopDrag;
        }
    }

    public void Detach()
    {
        isPossessed = false;
        followTarget = null;
        recentlyDetached = true;
    }

    public void Possess(Transform target)
    {
        isPossessed = true;
        followTarget = target;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    //public void Detach()
    //{
    //    isPossessed = false;
    //    followTarget = null;
    //}
}
