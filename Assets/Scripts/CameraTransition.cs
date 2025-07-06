using UnityEngine;
using System.Collections;

public class CameraTransition : MonoBehaviour
{
    public GameObject menuCamera;    
    public GameObject mainCamera;    
    public float transitionDuration = 2f;
    public AnimationCurve curve;

    private float timeElapsed;
    private bool transitioning;

    void Start()
    {
        if (mainCamera != null)
            mainCamera.SetActive(false); 
        transitioning = false;
    }

    public void StartTransition()
    {
        if (!transitioning)
        {
            StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition()
    {
        transitioning = true;

        Camera menuCam = menuCamera.GetComponent<Camera>();
        Camera mainCam = mainCamera.GetComponent<Camera>();

        Vector3 startPos = menuCamera.transform.position;
        Quaternion startRot = menuCamera.transform.rotation;
        Vector3 endPos = mainCamera.transform.position;
        Quaternion endRot = mainCamera.transform.rotation;

        mainCamera.SetActive(true);

        while (timeElapsed < transitionDuration)
        {
            float t = curve.Evaluate(timeElapsed / transitionDuration);
            menuCamera.transform.position = Vector3.Lerp(startPos, endPos, t);
            menuCamera.transform.rotation = Quaternion.Lerp(startRot, endRot, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        menuCamera.SetActive(false);
        transitioning = false;
    }
}
