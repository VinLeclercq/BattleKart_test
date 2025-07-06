using UnityEngine;

public class MenuActions : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject gameUI;
    public GameObject menuCamera;
    public GameObject mainCamera;
    public GameObject tutorialImage;
    public CameraTransition cameraTransition; // Drag ton script dans l’inspecteur

    private bool isInGame = false;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void Update()
    {
        // Si on est en jeu et que l’utilisateur appuie sur Échap, retour au menu
        if (isInGame && Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu();
        }
    }

    public void PlayGame()
    {
        if (cameraTransition != null)
            cameraTransition.StartTransition(); 

        // UI
        gameUI.SetActive(true);
        menuCanvas.SetActive(false);

        // Souris
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        isInGame = true;
    }

    public void BackToMenu()
    {
        // Revenir à la caméra menu
        mainCamera.SetActive(false);
        menuCamera.SetActive(true);

        // Réactiver le menu et désactiver l’interface de jeu
        gameUI.SetActive(false);
        menuCanvas.SetActive(true);

        // Réafficher la souris dans le menu
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        isInGame = false;
    }

    private System.Collections.IEnumerator ShowTutorial()
    {
        tutorialImage.SetActive(true);
        CanvasGroup cg = tutorialImage.GetComponent<CanvasGroup>();
        if (cg == null)
        {
            cg = tutorialImage.AddComponent<CanvasGroup>();
        }
        cg.alpha = 1f;

        yield return new WaitForSeconds(3f);

        float fadeTime = 1f;
        float t = 0;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(1, 0, t / fadeTime);
            yield return null;
        }
        tutorialImage.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
