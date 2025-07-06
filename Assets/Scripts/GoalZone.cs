using UnityEngine;

public class GoalZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddGoal();
                Debug.Log("But !");
            }
            else
            {
                Debug.LogError("GameManager.Instance est null !");
            }
        }
    }
}
