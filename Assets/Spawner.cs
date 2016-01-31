using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies = new GameObject[20];
    public float spawnRate;
    public float currentSpawnTimer;
    public int currentEnemiesAlive;
    public int maxEnemiesAlive;
    public GameObject enemyToSpawn;
    Rect minimumSpawnPerimeter;
    Rect maximiumSpawnPerimeter;
    Vector2 spawnPos;
    float currentRand;

    public bool shouldSpawn = false;

    // Use this for initialization
    void Start()
    {
        minimumSpawnPerimeter = maximiumSpawnPerimeter = new Rect(Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)), Camera.main.ViewportToWorldPoint(new Vector3(1.5f, 1.5f, 0)));
        maximiumSpawnPerimeter.width *= 1.25f;
        maximiumSpawnPerimeter.height *= 1.25f;
        maximiumSpawnPerimeter.center = minimumSpawnPerimeter.center;


        currentSpawnTimer = spawnRate;
    }
    public void KillAll()
    {
        foreach (GameObject go in enemies)
        {
            Destroy(go);
        }
    }
    void FixedUpdate()
    {
        if (shouldSpawn)
        {
            spawnPos.x = (Random.value * maximiumSpawnPerimeter.width) + maximiumSpawnPerimeter.xMin;
            spawnPos.y = (Random.value * maximiumSpawnPerimeter.height) + maximiumSpawnPerimeter.yMin;

            if (!minimumSpawnPerimeter.Contains(spawnPos) && (currentSpawnTimer <= 0) && (currentEnemiesAlive < maxEnemiesAlive))
            {
                Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);
                currentEnemiesAlive++;
                currentSpawnTimer = spawnRate;
                spawnPos = minimumSpawnPerimeter.center;
            }

            currentSpawnTimer -= Time.fixedDeltaTime;
            Debug.DrawLine(minimumSpawnPerimeter.min, minimumSpawnPerimeter.max);
            Debug.DrawLine(maximiumSpawnPerimeter.min, maximiumSpawnPerimeter.max);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
