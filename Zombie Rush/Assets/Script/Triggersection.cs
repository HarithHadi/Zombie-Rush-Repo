using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggersection : MonoBehaviour
{
    public GameObject[] roadSection;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            int randomindex = Random.Range(0, roadSection.Length);

            Transform current = other.transform.parent;
            Transform spawnpoint = current.Find("SpawnPoint");

            if (spawnpoint != null)
            {

                Instantiate(roadSection[randomindex], spawnpoint.position, Quaternion.identity);
                //Instantiate(roadSection[0], new Vector3(0, 0, -61.46f), Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("SpawnPoint not found in map section!");
            }
        }
    }
}
