using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Canvases")]
    public Canvas gameplayCanvas;
    public Canvas menuCanvas;

    [Header("Gameplay UI")]
    public Slider powerBar;
    public TMP_Text scoreText;
    public TMP_Text timerText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (gameplayCanvas == null || menuCanvas == null)
        {
            Debug.LogError("Les canvases ne sont pas assignés dans l'inspecteur !");
        }

        if (powerBar == null || scoreText == null || timerText == null)
        {
            Debug.LogError("Les éléments de l'interface ne sont pas assignés dans l'inspecteur !");
        }

        gameplayCanvas.enabled = false;
        menuCanvas.enabled = true;
    }

    public void SetPower(float value)
    {
        if (powerBar != null)
            powerBar.value = value;
    }

    public void ShowPowerBar(bool visible)
    {
        if (powerBar != null)
            powerBar.gameObject.SetActive(visible);
    }

    public void SetScore(int score)
    {
        if (scoreText != null)
            scoreText.text = "Score : " + score;
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
        if (menuCanvas != null) menuCanvas.enabled = false;
        if (gameplayCanvas != null) gameplayCanvas.enabled = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
