using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public GameObject ballObject;
    private Ball ball;
    private Transform ballHoldPoint;

    [Header("Possession")]
    public bool hasBall = false;
    private bool canRepossess = true;

    [Header("Tir & Passe")]
    private float currentPower = 0f;
    public float maxPower = 1f;
    public float chargeSpeed = 1f;

    [Tooltip("Force maximale du tir")]
    public float shootForce = 2f;

    [Tooltip("Force de la passe")]
    public float passForce = 5f;

    [Header("Cooldowns")]
    public float actionCooldown = 0.5f;
    private float lastActionTime = 0f;
    public float possessionDelay = 1f;

    void Start()
    {
        if (ballObject == null)
            ballObject = GameObject.FindWithTag("Ball");

        if (ballObject != null)
            ball = ballObject.GetComponent<Ball>();
        else
            Debug.LogWarning("Ball not found!");

        ballHoldPoint = new GameObject("BallHoldPoint").transform;
        ballHoldPoint.SetParent(transform);
        ballHoldPoint.localPosition = new Vector3(0, 0.477f, 0.6f);
    }

    void Update()
    {
        if (!hasBall && canRepossess && ball != null)
        {
            float dist = Vector3.Distance(ball.transform.position, transform.position);
            if (dist < 1.2f)
                PossessBall();
        }

        // Chargement du tir
        if (hasBall && Input.GetMouseButton(0))
        {
            currentPower += Time.deltaTime * chargeSpeed;
            currentPower = Mathf.Clamp(currentPower, 0, maxPower);
            UIManager.Instance.ShowPowerBar(true);
            UIManager.Instance.SetPower(currentPower);
        }

        // Tir
        if (hasBall && Input.GetMouseButtonUp(0))
        {
            StartCoroutine(Shoot(currentPower));
            currentPower = 0f;
            UIManager.Instance.SetPower(0f);
            UIManager.Instance.ShowPowerBar(false);
        }

        // Passe
        if (hasBall && Input.GetMouseButtonDown(1))
        {
            StartCoroutine(Pass());
        }
    }

    void PossessBall()
    {
        hasBall = true;
        ball.Possess(ballHoldPoint);
    }

    IEnumerator Shoot(float power)
    {
        if (Time.time - lastActionTime < actionCooldown) yield break;
        hasBall = false;
        canRepossess = false;
        ball.Detach();
        yield return new WaitForFixedUpdate();
        yield return null;
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Vector3 shootDirection = transform.forward + Vector3.up * 0.3f;
        float forceMagnitude = power * shootForce;
        Debug.Log("Shoot force magnitude: " + forceMagnitude);
        rb.AddForce(shootDirection.normalized * forceMagnitude, ForceMode.Impulse);
        lastActionTime = Time.time;
        yield return new WaitForSeconds(possessionDelay);
        canRepossess = true;
    }

    IEnumerator Pass()
    {
        if (Time.time - lastActionTime < actionCooldown) yield break;
        hasBall = false;
        canRepossess = false;
        ball.Detach();
        yield return new WaitForFixedUpdate();
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        float forceMagnitude = passForce;
        Debug.Log("Pass force magnitude: " + forceMagnitude);
        rb.AddForce(transform.forward * forceMagnitude, ForceMode.Impulse);
        lastActionTime = Time.time;
        yield return new WaitForSeconds(possessionDelay);
        canRepossess = true;
    }
}

