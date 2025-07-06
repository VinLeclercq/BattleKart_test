using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Slider powerBar;
    private TMP_Text scoreText;
    private TMP_Text timerText;

    private Canvas gameplayCanvas;
    private Canvas menuCanvas;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        gameplayCanvas = GameObject.Find("GamePlayUI")?.GetComponent<Canvas>();
        menuCanvas = GameObject.Find("MenuUI")?.GetComponent<Canvas>();

        if (menuCanvas == null || gameplayCanvas == null)
        {
            Debug.LogWarning("GameplayCanvas ou nemuCanavas pas trouvé");
        }
        if (gameplayCanvas != null)
        {
            Transform root = gameplayCanvas.transform;

            powerBar = root.Find("PowerBar")?.GetComponent<Slider>();
            scoreText = root.Find("ScoreText")?.GetComponent<TMP_Text>();
            timerText = root.Find("TimerText")?.GetComponent<TMP_Text>();

            if (powerBar == null || scoreText == null || timerText == null)
            {
                Debug.LogWarning("Un ou plusieurs éléments UI sont introuvables dans GameplayUI.");
            }
        }
        else
        {
            Debug.LogError("GameplayUI introuvable.");
        }

        gameplayCanvas.enabled = false;
        menuCanvas.enabled = true;
    }

    public void SetPower(float value)
    {
        if (powerBar != null)
        {
            powerBar.value = value;
        }
    }

    public void ShowPowerBar(bool visible)
    {
        if (powerBar != null)
        {
            powerBar.gameObject.SetActive(visible);
        }
    }

    public void SetScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score : " + score;
        }
    }

    public void SetTimer(float time)
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    public void SwitchToGameplay()
    {
        menuCanvas.enabled = false;
        gameplayCanvas.enabled = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
