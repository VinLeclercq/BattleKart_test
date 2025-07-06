// =========================
// Player.cs
// =========================
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public GameObject ballObject;
    private Ball ball;
    private float ballRadius;

    public bool hasBall = false;
    private Transform ballHoldPoint;

    private float currentPower = 0f;
    public float maxPower = 1f;
    public float chargeSpeed = 1f;
    public float shootForce = 10f;
    public float passForce = 5f;

    private float lastActionTime = 0f;
    public float actionCooldown = 0.5f;

    void Start()
    {
        if (ballObject == null)
        {
            ballObject = GameObject.FindWithTag("Ball");
        }

        if (ballObject != null)
        {
            ball = ballObject.GetComponent<Ball>();
            ballRadius = ballObject.GetComponent<SphereCollider>().radius;
        }
        else
        {
            Debug.LogWarning("No Ball found in the scene.");
        }

        ballHoldPoint = new GameObject("BallHoldPoint").transform;
        ballHoldPoint.SetParent(transform);
        ballHoldPoint.localPosition = new Vector3(0, 0.477f, 0.6f);
    }

    void Update()
    {
        if (hasBall && ball != null)
        {
            ball.Possess(transform, ballHoldPoint);
        }

        if (!hasBall && ball != null)
        {
            float distanceToBall = Vector3.Distance(ball.transform.position, transform.position);
            if (distanceToBall < 1.2f)
            {
                hasBall = true;
            }
        }

        if (hasBall && Input.GetMouseButton(0))
        {
            currentPower += Time.deltaTime * chargeSpeed;
            currentPower = Mathf.Clamp(currentPower, 0, maxPower);
            UIManager.Instance.ShowPowerBar(true);
            UIManager.Instance.SetPower(currentPower);
        }

        if (hasBall && Input.GetMouseButtonUp(0))
        {
            Shoot(currentPower);
            currentPower = 0f;
            UIManager.Instance.SetPower(0f);
            UIManager.Instance.ShowPowerBar(false);
        }

        if (hasBall && Input.GetMouseButtonDown(1))
        {
            Pass();
        }
    }

    void Shoot(float power)
    {
        if (Time.time - lastActionTime < actionCooldown || ball == null) return;
        StartCoroutine(ShootAfterDetach(power));
    }

    IEnumerator ShootAfterDetach(float power)
    {
        ball.DetachFromPlayer();
        hasBall = false;
        yield return new WaitForFixedUpdate(); // attendre une frame physique

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * power * shootForce, ForceMode.Impulse);

        lastActionTime = Time.time;
    }

    void Pass()
    {
        if (Time.time - lastActionTime < actionCooldown || ball == null) return;
        StartCoroutine(PassAfterDetach());
    }

    IEnumerator PassAfterDetach()
    {
        ball.DetachFromPlayer();
        hasBall = false;
        yield return new WaitForFixedUpdate(); // attendre une frame physique

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * passForce, ForceMode.Impulse);

        lastActionTime = Time.time;
    }


}
