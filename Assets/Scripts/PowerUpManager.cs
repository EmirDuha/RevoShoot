using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [Header("Spawn Data")]
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private Transform[] spawnPoint;

    [Header("Time Data")]
    private float spawnInterval;
    [SerializeField] private float minInterval = 2f;
    [SerializeField] private float maxInterval = 5f;

    private void Start()
    {
        RandomizeSpawnInterval();
    }

     private void Update()
    {
        spawnInterval -= Time.deltaTime;
        if (spawnInterval <= 0f)
        {
            CreatePowerUp();
            RandomizeSpawnInterval();
        }
    }

    private void CreatePowerUp()
    {
        Transform randomSpawnPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];
        GameObject powerup = Instantiate(powerUpPrefab, randomSpawnPoint.position, Quaternion.identity);
        powerup.AddComponent<PowerUpFall>();
    }

    private void RandomizeSpawnInterval()
    {
        spawnInterval = Random.Range(minInterval, maxInterval);
    }


}
