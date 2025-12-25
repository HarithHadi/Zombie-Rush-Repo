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
            Instantiate(roadSection[randomindex], new Vector3(0, 0, -61.46f), Quaternion.identity);
        }
    }
}
