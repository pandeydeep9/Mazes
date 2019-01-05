using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;



public class PlayerController : MonoBehaviour {

    Transform changedTransform;
    Transform detectedTransform;
    float xPrevVal;
    //TextToFile tf = new TextToFile();

    int direction = 0;//1-Foreward 2-Backward 3-Rotate Left 4-Rotate Right
    private void Move(int direction)
    {
        transform.Translate(Vector3.forward * direction * Time.deltaTime);
        Debug.Log(transform.position.x);

    }
    private void Rotate(int direction)
    {
        transform.Rotate(Vector3.up*direction);
    }
    private void Start()
    {

        changedTransform = transform;
        detectedTransform = changedTransform;
        xPrevVal = transform.position.x;
    }

    private void Update()
    {
        if(xPrevVal != transform.position.x)
        {
            Debug.Log("Hello");
            SaveToFile(transform.position.x, transform.position.z);
            detectedTransform.position = changedTransform.position;
            xPrevVal = transform.position.x;
        }
        
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

    private const string FILE_NAME = "MyFile.txt";
    public static void SaveToFile(float x, float z)
    {
        StreamWriter sr;
        if (!File.Exists(FILE_NAME))
        {
            Console.WriteLine("{0} already exists.", FILE_NAME);
            sr = File.CreateText(FILE_NAME);
        }
        if (File.Exists(FILE_NAME))
        {
            //Read the last line from the file and compare values with x, z, store only if the position of the player has changed.
        }
        sr = File.AppendText(FILE_NAME);
        sr.WriteLine("playerTransformPositions: x = {0}, z = {1} ",x,z
            );
        sr.Close();
    }
}
