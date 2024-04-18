using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite1Script : MonoBehaviour
{
    public Transform planetTarget;

    public float rotationSpeed = 5f;

    //make it so that it rotates at an angle
    private Vector3 rotationAxis = new Vector3(1, 1, 1);

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(planetTarget.position, rotationAxis, rotationSpeed * Time.deltaTime);
        //transform.RotateAround(planetTarget.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
