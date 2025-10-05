using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private Transform spawnPoint;
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
        GameObject powerup = Instantiate(powerUpPrefab, spawnPoint.position, Quaternion.identity);
        powerup.AddComponent<PowerUpFall>();
    }

    private void RandomizeSpawnInterval()
    {
        spawnInterval = Random.Range(minInterval, maxInterval);
    }


}
