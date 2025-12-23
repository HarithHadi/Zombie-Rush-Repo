using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)) {

            transform.Translate(Vector3.left * Time.deltaTime * speed, Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right* Time.deltaTime * speed, Space.Self);
        }
    }
}
