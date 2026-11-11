using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] ParticleSystem speedupParticleSystem;
    [SerializeField] float minFOV = 20f;
    [SerializeField] float maxFOV = 120f;
    [SerializeField] float zoomDuration = 1f;
    [SerializeField] float zoomSpeedModifier = 5;

    CinemachineCamera cinemachineCamera;

    private void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    public void ChangeCameraFOV(float speedAmount)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFOVCoroutine(speedAmount));
        if(speedAmount > 0)
        {
            speedupParticleSystem.Play();
        }
    }

    IEnumerator ChangeFOVCoroutine(float speedAmount)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float targetFOV = Mathf.Clamp(startFOV + speedAmount * zoomSpeedModifier, minFOV, maxFOV);
        float elapsedTime = 0f;
        while (elapsedTime < zoomDuration)
        {
            float t = elapsedTime / zoomDuration;
            elapsedTime += Time.deltaTime;
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, elapsedTime / zoomDuration);
            yield return null;
        }
        cinemachineCamera.Lens.FieldOfView = targetFOV;
    }
}
