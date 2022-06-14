using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgent : MonoBehaviour
{
    public float Speed = 10;

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(xAxis, 0, yAxis) * Time.deltaTime * Speed);
    }
}
