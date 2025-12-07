using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int maxEnemies = 8;
    public float spawnInterval = 5f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            int existing = GameObject.FindObjectsOfType<EnemyAI>().Length;
            if (existing < maxEnemies)
            {
                Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(enemyPrefab, sp.position, sp.rotation);
            }
        }
    }
}
