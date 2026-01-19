using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager instance;

    public float baseSpeed = 25f;
    public float difficultyRate = 3f;
    public float maxSpeed = 100f;

    private float increaseInterval = 5f;
    private float timer;

    public float movementSpeed = 8f;
    public float difficultyMoveRate = 2f;
    public float maxMove = 20f;

    void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= increaseInterval) 
        {
            timer = 0f;
            baseSpeed = Mathf.Min(baseSpeed + difficultyRate, maxSpeed);
            movementSpeed = Mathf.Min(movementSpeed + difficultyMoveRate, maxMove);
        }
    }

    public float GetSpeed()
    {
        return baseSpeed;
    }

    public float getMoveSpeed() 
    {
        return movementSpeed;
    }
}
