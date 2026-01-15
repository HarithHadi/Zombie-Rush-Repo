using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoFast : MonoBehaviour
{
    public GameObject pickupEffect;
    private int fastAmount = 2;
    private float boostDuration = 2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Pickup(other);
        }
    }

    void Pickup(Collider player) 
    {
        //Check if boost still happenin
        //if it is keep the effect going
        if (SpeedBoostManager.instance != null) 
        {
            SpeedBoostManager.instance.ActiveSpeedBoost(fastAmount, boostDuration, player);
        }
        Destroy(gameObject);
        //Destroy
    }
}
