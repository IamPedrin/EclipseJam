using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform[] spawnPoints;

    public float initialMinSpawnTime = 5f;
    public float initialMaxSpawnTime = 10f;
    public float difficultyIncreaseRate = 0.75f;

    public float minSpawnTime;
    public float maxSpawnTime;
    private float timeUntilSpawn;

    private void Start()
    {
        minSpawnTime = initialMinSpawnTime;
        maxSpawnTime = initialMaxSpawnTime;
        SetTimeUntilSpawn();
    }

    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if(timeUntilSpawn <= 0)
        {
            SpawnEnemy();
            AdjustDiff();  
            SetTimeUntilSpawn();
        }
    }

    private void SpawnEnemy()
    {
        if (spawnPoints.Length == 0 || enemies.Length == 0)
        {
            Debug.LogWarning("Spawn ou Enemies Quebraram!");
            return;
        }

        //Escolhe um Spawner
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        //Escolhe um inimigo
        GameObject enemyToSpawn = enemies[Random.Range(0, enemies.Length)];

        //Instancia inimigo
        Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);
    }

    private void AdjustDiff()
    {
        //Diminui tempo para aumentar dificuldade
        minSpawnTime *= difficultyIncreaseRate;
        maxSpawnTime *= difficultyIncreaseRate;

        //Garantir dificuldade máxima
        minSpawnTime = Mathf.Max(minSpawnTime, 0.5f);
        maxSpawnTime = Mathf.Max(maxSpawnTime, 1f);
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }
}
