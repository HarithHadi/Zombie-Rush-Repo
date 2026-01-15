using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPowerup : MonoBehaviour
{
    public GameObject pickupEffect;
    public int healAmount = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Pickup(other);
        }
    }

    void Pickup(Collider player) 
    {
        //Cool effect
        Instantiate(pickupEffect, player.transform.position, transform.rotation);

        //Effect of the powerup
        CollisionDetector playerstats = player.GetComponent<CollisionDetector>();
        playerstats.Heal(healAmount);

        //remove powerupObject
        Destroy(gameObject);
    }
}
