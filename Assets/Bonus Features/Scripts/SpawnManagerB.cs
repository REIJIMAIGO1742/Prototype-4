using UnityEngine;
using UnityEngine.PlayerLoop;

public class SpawnManagerB : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject[] powerUpPrefab;
    public GameObject miniBoss;
    private float spawnRange = 9f;
    public int enemyCount;
    public int waveNumber = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int powerUpIndex = Random.Range(0, powerUpPrefab.Length);
        SpawnEnemyWave(waveNumber);
        Instantiate(powerUpPrefab[powerUpIndex], GennerateSpawnPosition(), powerUpPrefab[powerUpIndex].transform.rotation);
    }

    private void Update()
    {
        enemyCount = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;
        if (enemyCount == 0 && waveNumber < 3)
        {
            int powerUpIndex = Random.Range(0, powerUpPrefab.Length);
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerUpPrefab[powerUpIndex], GennerateSpawnPosition(), powerUpPrefab[powerUpIndex].transform.rotation);
        }
        else if (enemyCount == 0 )
        {
            int powerUpIndex = Random.Range(0, powerUpPrefab.Length);
            waveNumber = 0;
            SpawnMiniBoss();
            Instantiate(powerUpPrefab[powerUpIndex], GennerateSpawnPosition(), powerUpPrefab[powerUpIndex].transform.rotation);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i ++)
        {
            int enemyIndex = Random.Range(0,enemyPrefab.Length);
            Instantiate(enemyPrefab[enemyIndex], GennerateSpawnPosition(), enemyPrefab[enemyIndex].transform.rotation);
        }
    }
    void SpawnMiniBoss()
    {
            int enemyIndex = Random.Range(0, enemyPrefab.Length);
            Instantiate(miniBoss, GennerateSpawnPosition(), miniBoss.transform.rotation);
    }


    private Vector3 GennerateSpawnPosition ()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;

    }

    
}
