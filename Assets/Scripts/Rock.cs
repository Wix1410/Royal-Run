using UnityEngine;
using Unity.Cinemachine;

public class Rock : MonoBehaviour
{
    [SerializeField] ParticleSystem collisionParticleSystem;
    [SerializeField] AudioSource boulderSmashAudioSource;
    [SerializeField] float shakeModifier = 10f;
    [SerializeField] float collisonCooldown = 1f;

    CinemachineImpulseSource cinemachineImpulseSource;

    float collisionTimer = 0f;

    void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        collisionTimer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collisionTimer < collisonCooldown) return; // Prevent multiple collisions in quick succession
        FireImpulse();
        CollisionFX(collision);
        collisionTimer = 0f; // Reset the collision timer
    }

    private void FireImpulse()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = (1 / distance) * shakeModifier; // Adjust the shake intensity based on distance
        shakeIntensity = Mathf.Min(shakeIntensity, 1f);
        cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
    }

    void CollisionFX(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        collisionParticleSystem.transform.position = contactPoint.point;
        collisionParticleSystem.Play();
        boulderSmashAudioSource.Play();
    }
}
