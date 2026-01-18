using UnityEngine;

public class ZombieFBMenu : MonoBehaviour
{
    private Vector3 startPos;

    private float distance;
    private float speed;
    private float timeOffset;

    void Start()
    {
        startPos = transform.position;

        distance = Random.Range(3f, 10f);      // how far it moves
        speed = Random.Range(1.5f, 9f);       // how fast it moves
        timeOffset = Random.Range(0f, 10f);   // desync movement
    }

    void Update()
    {
        float move = Mathf.PingPong((Time.time + timeOffset) * speed, distance);
        transform.position = startPos + transform.forward * move;
    }
}
