using UnityEngine;
using System.Collections.Generic;

public class Chunk : MonoBehaviour
{
    List<int> availableLanes = new List<int> { 0, 1, 2 };

    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;

    [SerializeField] float[] lanes = {-2.5f, 0f, 2.5f };
    [SerializeField] float appleSpawnChance = 0.3f;
    [SerializeField] float coinSpawnChance = 0.5f;
    [SerializeField] float coinSeparationLength = 2f;

    LevelGenerator levelGenerator;
    ScoreMenager scoreMenager;

    private void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoin();
    }

    public void Init(LevelGenerator levelGenerator, ScoreMenager scoreMenager)
    {
        this.levelGenerator = levelGenerator;
        this.scoreMenager = scoreMenager;
    }

    void SpawnFences()
    {
        int fencesToSpawn = Random.Range(0, lanes.Length);
        for(int i = 0; i < fencesToSpawn; i++)
        {
            if (availableLanes.Count <= 0) break;
            int randomLaneIndex = SelectLane();
            Vector3 spawnPos = new Vector3(lanes[randomLaneIndex], transform.position.y, transform.position.z);
            Instantiate(fencePrefab, spawnPos, Quaternion.identity, this.transform);
        }
    }

    void SpawnApple()
    {
        if(Random.value > appleSpawnChance || availableLanes.Count <= 0) return;
        int randomLaneIndex = SelectLane();
        Vector3 spawnPos = new Vector3(lanes[randomLaneIndex], transform.position.y, transform.position.z);
        Apple newApple = Instantiate(applePrefab, spawnPos, Quaternion.identity, this.transform).GetComponent<Apple>();
        newApple.Init(levelGenerator);
    }

    void SpawnCoin()
    {
        if (Random.value > coinSpawnChance || availableLanes.Count <= 0) return;
        int randomLaneIndex = SelectLane();
        int maxCoinsToSpawn = 6;
        int coinsToSpawn = Random.Range(1, maxCoinsToSpawn);
        float topOfChunkZ = transform.position.z + (coinSeparationLength * 2);
        for (int i = 0; i < coinsToSpawn; i++)
        {
            float spawnPosZ = topOfChunkZ - (i * coinSeparationLength);
            Vector3 spawnPos = new Vector3(lanes[randomLaneIndex], transform.position.y, spawnPosZ);
            Coin newCoin = Instantiate(coinPrefab, spawnPos, Quaternion.identity, this.transform).GetComponent<Coin>();
            newCoin.Init(scoreMenager);
        }
    }

    int SelectLane()
    {
        int randomLaneIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];
        availableLanes.RemoveAt(randomLaneIndex);
        return selectedLane;
    }

}
