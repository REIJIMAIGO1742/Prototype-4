using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MiniBossPower : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    private GameObject spawnPoint;

    private float spawnRange = 9f;


    private void Start()
    {
        spawnPoint = GameObject.Find("SpawnManager");
        StartCoroutine(SpawnEnemy());
    }


    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(5);
        while (gameObject != null)
        {
            for (int i = 0; i < 3; i++)
            {
                int enemyIndex = Random.Range(0, enemyPrefab.Length);
                Instantiate(enemyPrefab[enemyIndex], GennerateSpawnPosition(), enemyPrefab[enemyIndex].transform.rotation);
                
            }
            yield return new WaitForSeconds(5);
        }
        
    }


    private Vector3 GennerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = spawnPoint.transform.position + new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;

    }


}
