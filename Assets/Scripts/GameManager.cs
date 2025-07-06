using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private float gameTime = 0f;
    private int score = 0;
    private bool isGameActive = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (isGameActive)
        {
            gameTime += Time.deltaTime;
            UIManager.Instance.SetTimer(gameTime);
        }
    }

    public void AddGoal()
    {
        score++;
        UIManager.Instance.SetScore(score);
    }

    public void StartGame()
    {
        isGameActive = true;
        gameTime = 0f;
        score = 0;
    }
}
