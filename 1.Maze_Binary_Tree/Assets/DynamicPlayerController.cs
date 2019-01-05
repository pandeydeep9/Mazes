using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPlayerController : MonoBehaviour {

    private float startPositionX, startPositionZ;

    public int zNewNeeded, xNewNeeded;

	// Use this for initialization
	void Start () {
        startPositionX = transform.position.x;
        startPositionZ = transform.position.z;
	}
    private void MoveForeward(int direction)
    {
        //Debug.Log(Vector3.forward * direction * Time.deltaTime);
        //Debug.Log(startPositionX + "nad z" + startPositionZ);
        if (Mathf.Abs(startPositionZ - transform.position.z) > 1f)
        {
            //Debug.Log("Too Much Movement on z");
            zNewNeeded = (startPositionZ - transform.position.z)>0?1:-1;
            startPositionZ = transform.position.z;
        }
        transform.Translate(Vector3.forward * direction * Time.deltaTime);
    }
    private void MoveSideways(int direction)
    {
        if (Mathf.Abs(startPositionX - transform.position.x) > 1f)
        {
            //Debug.Log("Too Much Movement on X");
            xNewNeeded =(startPositionX - transform.position.x)>0?1:-1;
            startPositionX = transform.position.x;
            //DynamicGround
        }
        transform.Translate(Vector3.right * direction * Time.deltaTime);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveForeward(1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveSideways(1);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveForeward(-1);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveSideways(-1);
        }
    }

    public void resetxNewNeeded()
    {
        xNewNeeded = 0;
    }
    public void resetzNewNeeded()
    {
        zNewNeeded = 0;
    }
}
