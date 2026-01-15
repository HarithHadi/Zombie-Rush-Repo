using UnityEngine;

public enum SIDE { Left = 0, Mid = 1, Right = 2 }

public class PlayerMovement : MonoBehaviour
{
    public float[] lanes = { -3f, 0f, 3f };
    public float laneSpeed = 18f;

    public SIDE currentSide = SIDE.Mid;
    //public Animator carAnimator;

    private float targetX;
    private bool isMoving = false;

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
    }

    void HandleInput()
    {
        if (isMoving) return;

        // RIGHT key → move right
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentSide < SIDE.Right)
            {
                currentSide++;
                StartLaneChange(true);
            }
        }

        // LEFT key → move left
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentSide > SIDE.Left)
            {
                currentSide--;
                StartLaneChange(false);
            }
        }
    }

    void StartLaneChange(bool right)
    {
        targetX = lanes[(int)currentSide];
        isMoving = true;
        //carAnimator.speed = 1.8f;

        //if (right)
        //    carAnimator.Play("CarLeftDodge", 0,0f);
        //else
        //    carAnimator.Play("CarRightDodge", 0,0f);
    }

    void Move()
    {
        if (!isMoving) return;

        Vector3 pos = transform.position;
        pos.x = Mathf.MoveTowards(pos.x, targetX, laneSpeed * Time.deltaTime);
        transform.position = pos;

        if (Mathf.Abs(pos.x - targetX) < 0.01f)
        {
            pos.x = targetX;
            transform.position = pos;
            isMoving = false;

            // RESET animation
            //carAnimator.SetBool("TurnLeft", false);
            //carAnimator.SetBool("TurnRight", false);
        }
    }
}
