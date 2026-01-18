using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRealismAnim : MonoBehaviour
{
    public float swayAmount = 0.03f;
    public float swaySpeed = 4f;

    private float startY;

    void Start()
    {
        startY = transform.localPosition.y;
    }

    void Update()
    {
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;

        Vector3 pos = transform.localPosition;
        pos.y = startY + sway;
        transform.localPosition = pos;
    }
}
