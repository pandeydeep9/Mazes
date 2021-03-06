﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour {
    int direction = 0;//1-Foreward 2-Backward 3-Rotate Left 4-Rotate Right

    public float speed = 2f;
    private void Move(int direction)
    {
        transform.Translate(Vector3.forward * direction * Time.deltaTime * speed);
    }
    private void Rotate(int direction)
    {
        transform.Rotate(Vector3.up*direction);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Move(1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Rotate(1);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Move(-1);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Rotate(-1);
        }
    }
}
