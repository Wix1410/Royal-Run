using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float roationSpeed = 100f;
    const string playerString = "Player";

    private void Update()
    {
        transform.Rotate(0f, roationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerString))
        {
            OnPickup();
            Destroy(gameObject);
        }
    }

    protected abstract void OnPickup();
}
