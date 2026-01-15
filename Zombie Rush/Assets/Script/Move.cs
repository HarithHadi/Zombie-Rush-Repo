using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public int speed = 1;
    public int speedMultiplier = 1;
    void Start()
    {
        if (SpeedBoostManager.instance != null) 
        {
            speed = SpeedBoostManager.getCurrentSped();
        }
        else 
        {
            speed = 1;
        }
    }

    
    void Update()
    {
        
        //Add into X to make the game faster
        transform.position += Vector3.forward * 25 * speed * Time.deltaTime;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroy")) { 
            Destroy(gameObject);
        }
    }

    public void setSpeed(int newSpeed) 
    {
        speed = newSpeed;
    }
}
