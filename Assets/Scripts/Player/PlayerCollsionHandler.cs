using UnityEngine;

public class PlayerCollsionHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float collisonCoolDown = 1f;
    [SerializeField] float ajustChangeMoveSpeedAmount = -2f;

    LevelGenerator levelGenerator;

    const string hitString = "Hit";

    float cooldownTimer;

    private void Start()
    {
        levelGenerator = FindAnyObjectByType<LevelGenerator>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (cooldownTimer < collisonCoolDown) return;

        levelGenerator.ChangeChunkMoveSpeed(ajustChangeMoveSpeedAmount);
        animator.SetTrigger(hitString);
        cooldownTimer = 0f;
    }
}
