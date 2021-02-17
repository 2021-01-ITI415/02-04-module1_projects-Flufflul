using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        // transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
        // transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
        // transform.Rotate(new Vector3(15, 0, 0) * Time.deltaTime);
    }
}
