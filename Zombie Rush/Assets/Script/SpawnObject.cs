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
    private int[] lanes = { -3, 0, 3 };
    public int obstacleCount = 10;
    private int[] zPos = { 0, -30, -60, -90, -120};

    void Start()
    {
        SpawnItems();
    }


    public void SpawnItems()
    {
        List<int> availableLanes = new List<int>(lanes);

        foreach (int Zcurr in zPos) 
        {
            int ScoreIndex = Random.Range(0, availableLanes.Count);
            
            Vector3 spawnPos = new Vector3(availableLanes[ScoreIndex], 0.65f, Zcurr);

            GameObject obj = Instantiate(zombies, transform);

            obj.transform.localPosition = spawnPos;
            
            bool powerupdy = false;
            foreach (int lane in availableLanes) 
            {
                if (lane == availableLanes[ScoreIndex]) continue;

                else 
                {
                    float power = Random.Range(0f, 1.0f);

                    if (power <= 0.1f && !powerupdy)
                    {
                        Debug.Log("spawning powerup");
                        Vector3 powerPos = new Vector3(lane, 0.65f, Zcurr);
                        int prefabIndex = Random.Range(0, powerup.Length);
                        GameObject obs = Instantiate(powerup[prefabIndex], transform);
                        obs.transform.localPosition = powerPos;
                        powerupdy = true;
                    }
                    else 
                    {
                        Vector3 obstaclePos = new Vector3(lane, 0.65f, Zcurr);
                        int prefabIndex = Random.Range(0, obstacles.Length);

                        GameObject obs = Instantiate(obstacles[prefabIndex], transform);
                        obs.transform.localPosition = obstaclePos;
                    }
                }
            }
        }


    }

}
