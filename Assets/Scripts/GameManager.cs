using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Références")]
    public Transform ball;
    public Transform ballSpawnCenter;
    public Transform field;

    [Header("Paramètres")]
    public float outOfBoundsMargin = 2f;

    private float gameTime = 0f;
    private int score = 0;
    private bool isGameActive = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (isGameActive)
        {
            gameTime += Time.deltaTime;
            UIManager.Instance.SetTimer(gameTime);
        }

        CheckOutOfBounds(); // Vérifie si la balle est sortie du terrain
    }

    public void AddGoal()
    {
        score++;
        UIManager.Instance.SetScore(score);
        RespawnBall(); // Réapparaît la balle au centre
    }

    public void StartGame()
    {
        isGameActive = true;
        gameTime = 0f;
        score = 0;
        UIManager.Instance.SetScore(score);
    }

    private void RespawnBall()
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        ball.position = ballSpawnCenter.position;
        Debug.Log("Respawning ball at center: " + ballSpawnCenter.position);
    }

    private void CheckOutOfBounds()
    {
        if (field == null || ball == null) return;

        Collider fieldCollider = field.GetComponent<Collider>();
        if (fieldCollider == null)
        {
            Debug.LogError("Le terrain n'a pas de collider !");
            return;
        }

        Bounds bounds = fieldCollider.bounds;

        // Si la balle est en dehors des bounds
        if (!bounds.Contains(ball.position))
        {
            Vector3 newPosition = ball.position;

            // Clamping dans les limites avec marge
            newPosition.x = Mathf.Clamp(ball.position.x, bounds.min.x + outOfBoundsMargin, bounds.max.x - outOfBoundsMargin);
            newPosition.z = Mathf.Clamp(ball.position.z, bounds.min.z + outOfBoundsMargin, bounds.max.z - outOfBoundsMargin);
            newPosition.y = ballSpawnCenter.position.y;

            Rigidbody rb = ball.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            ball.position = newPosition;

            Debug.Log("Ball was out of bounds. Moved to: " + newPosition);
        }
    }
}
