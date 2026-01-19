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
            Debug.Log("Boost enabled");
            speed = SpeedBoostManager.getCurrentSped();
        }
        else 
        {
            speed = 1;
        }
    }

    
    void Update()
    {

        float currentSpeed = DifficultyManager.instance.GetSpeed();
        transform.position += Vector3.forward * currentSpeed * speed * Time.deltaTime;    
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
