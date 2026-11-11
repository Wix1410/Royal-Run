using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] float checkpointTimeExtension = 5f;

    GameManager gameManager;

    const string playerString = "Player";   

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(playerString))
        {
            gameManager.IncreaseTime(checkpointTimeExtension);
        }
    }
}
