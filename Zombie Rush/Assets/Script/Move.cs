using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    void Update()
    {
        //Add into X to make the game faster
        transform.position += new Vector3(0, 0, 15) * Time.deltaTime;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroy")) { 
            Destroy(gameObject);
        }
    }
}
