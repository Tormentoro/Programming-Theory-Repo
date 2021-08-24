using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatRockWalls : MonoBehaviour
{
    private float zLowBound =  -36;
  
    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < zLowBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 30);
        }
    }
}
