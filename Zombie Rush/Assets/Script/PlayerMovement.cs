using System.Collections;
using UnityEngine;

public enum SIDE { Left = 0, Mid = 1, Right = 2 }

public class PlayerMovement : MonoBehaviour
{
    [Header("Lane Movement")]
    public float[] lanes = { -3f, 0f, 3f };
    public float laneSpeed = 18f;

    [Header("Steering Realism")]
    public float turnSpeed = 80f;
    public float steeringSmooth = 5f;

    public SIDE currentSide = SIDE.Mid;

    private float targetX;
    private bool isMoving = false;

    private float currentTurn;
    private float targetTurn;

    void Start()
    {
        targetX = lanes[(int)currentSide];
        Vector3 pos = transform.position;
        pos.x = targetX;
        transform.position = pos;
    }

    void Update()
    {
        HandleInput();
        Move();
        HandleSteering();
    }

    void HandleInput()
    {
        // Move LEFT
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentSide > SIDE.Left)
            {
                currentSide--;
                StartLaneChange(-1);
            }
        }

        // Move RIGHT
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentSide < SIDE.Right)
            {
                currentSide++;
                StartLaneChange(1);
            }
        }
    }

    void StartLaneChange(int direction)
    {
        targetX = lanes[(int)currentSide];
        isMoving = true;
        int steerdir = 0;
        
        if (direction == 1)
        {
            steerdir = -1;
        }
        else if (direction == -1) 
        {
            steerdir = 1;
        }
        // Steering direction
        targetTurn = steerdir * turnSpeed;
    }

    void Move()
    {
        if (!isMoving) return;

        Vector3 pos = transform.position;
        float movespeed = DifficultyManager.instance.getMoveSpeed();
        pos.x = Mathf.MoveTowards(pos.x, targetX, movespeed * Time.deltaTime);
        transform.position = pos;

        if (Mathf.Abs(pos.x - targetX) < 0.01f)
        {
            pos.x = targetX;
            transform.position = pos;
            isMoving = false;

            // Reset steering when done
            targetTurn = 0f;
        }
    }

    void HandleSteering()
    {
        // Smoothly interpolate currentTurn toward targetTurn
        currentTurn = Mathf.Lerp(currentTurn, targetTurn, steeringSmooth * Time.deltaTime * 1.5f);

        // Base rotation is 180° Y
        Quaternion baseRotation = Quaternion.Euler(0f, 180f, 0f);

        // Apply steering rotation on top of base rotation
        Quaternion steeringRotation = Quaternion.Euler(0f, 0f, currentTurn * 0.3f);


        transform.rotation = baseRotation * steeringRotation;
    }

    

}
