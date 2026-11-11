using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class LevelGenerator : MonoBehaviour
{
    List<GameObject> chunks = new List<GameObject>();

    [Header("Dependecies")]
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject[] chunkPrefabs;
    [SerializeField] GameObject checkpointChunkPrefab;
    [SerializeField] Transform chunkParent;
    [SerializeField] ScoreMenager scoreMenager;

    [Header("Level Settings")]
    [SerializeField] int startingChunksAmount = 12;
    [SerializeField] int checkpointChunkInterval = 8;
    [Tooltip("The length of each chunk, used to calculate spawn positions and when to destroy chunks.")]
    [SerializeField] float chunkLength = 10;
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float minMoveSpeed = 2f;
    [SerializeField] float maxMoveSpeed = 20f;
    [SerializeField] float minGravityZ = -22f;
    [SerializeField] float maxGravityZ = -2f;

    int chunksSpawned = 0;

    private void Start()
    {
        SpawnStartingChunks();
    }

    private void Update()
    {
        MoveChunks();
    }

    public void ChangeChunkMoveSpeed(float SpeedAmount)
    {
        float newMoveSpeed = moveSpeed + SpeedAmount;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed, minMoveSpeed, maxMoveSpeed);
        moveSpeed += SpeedAmount;
        if(newMoveSpeed != moveSpeed)
        {
            newMoveSpeed = moveSpeed;
            float newGravityZ = Physics.gravity.z - SpeedAmount;
            newGravityZ = Mathf.Clamp(newGravityZ, minGravityZ, maxGravityZ);
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newGravityZ);
            cameraController.ChangeCameraFOV(SpeedAmount);
        }
    }

    void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            SpawnChunk();
        }
    }

    float CalculateSpawnPositionZ()
    {
        float spawnPositionZ;
        if (chunks.Count == 0)
        {
            spawnPositionZ = transform.position.z;
        }
        else
        {
            spawnPositionZ = chunks[chunks.Count - 1].transform.position.z + chunkLength;
        }
        return spawnPositionZ;
    }

    void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));
            if(chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength) 
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
            }
        }
    }

    void SpawnChunk()
    {
        float spawnPositionZ = CalculateSpawnPositionZ();
        Vector3 chunkSpawnPos = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);
        GameObject chunkToSpawn = ChooseChunkToSpawn();
        GameObject newChunkGO = Instantiate(chunkToSpawn, chunkSpawnPos, Quaternion.identity, chunkParent);
        chunks.Add(newChunkGO);
        Chunk newChunk = newChunkGO.GetComponent<Chunk>();
        newChunk.Init(this, scoreMenager);
        chunksSpawned++;
    }

    private GameObject ChooseChunkToSpawn()
    {
        GameObject chunkToSpawn;
        if (chunksSpawned % checkpointChunkInterval == 0 && chunksSpawned != 0)
        {
            chunkToSpawn = checkpointChunkPrefab;
        }
        else
        {
            chunkToSpawn = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
        }

        return chunkToSpawn;
    }
}
