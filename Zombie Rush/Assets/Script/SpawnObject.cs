using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] obstacles;
    public GameObject zombies;
    public GameObject[] powerup;

    [Header("Settings")]
    public float[] lanes = { -3f, 0, 3f };
    public int obstacleCount = 10;

    private void Start()
    {
        SpawnItems();
    }
    public void SpawnObstacle()
    {
        for (int i = 0; i < obstacleCount; i++)
        {
            //1. Pick a random lane
            float randomX = lanes[Random.Range(0, lanes.Length)];
            float randomZ = Random.Range(-10f, 10f);

            Vector3 spawnPos = new Vector3(randomX, 0, randomZ);

            int prefabIndex = Random.Range(0, obstacles.Length);
            GameObject obs = Instantiate(obstacles[prefabIndex], transform.position + spawnPos, Quaternion.identity);

            obs.transform.SetParent(this.transform);

        }


    }

    void SpawnItems()
    {
        List<float> availableLanes = new List<float>(lanes);

        for (int i = 0; i < obstacleCount; i++)
        {
            if (availableLanes.Count == 0) break;

            int laneIndex = Random.Range(0, availableLanes.Count);
            float xPos = availableLanes[laneIndex];
            availableLanes.RemoveAt(laneIndex);

            float roll = Random.value; // returns 0.0 to 1.0
            GameObject selectedPrefab;

            if (roll < 0.10f)
            {
                selectedPrefab = powerup[Random.Range(0, powerup.Length)];
            }
            else if (roll < 0.55f)
            {
                selectedPrefab = obstacles[Random.Range(0, obstacles.Length)];
            }
            else
            {
                selectedPrefab = zombies;
            }

            float randomZ = Random.Range(-8f, 8f);
            Vector3 spawnPos = new Vector3(xPos, 0, randomZ);
            

            GameObject obj = Instantiate(selectedPrefab, transform.position + spawnPos, Quaternion.identity);
            obj.transform.SetParent(this.transform);
        }


    }

}
