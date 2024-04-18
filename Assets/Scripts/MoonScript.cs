using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour
{
    //pick the planet as the target
    public Transform planetTarget;

    //use axis to make it rotate on its axis
    public Transform axis;
    public float spinSpeed = 30f;
    public float rotationSpeed = 7f;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(planetTarget.position, Vector3.up, rotationSpeed * Time.deltaTime);
        transform.RotateAround(axis.position, Vector3.up, spinSpeed * Time.deltaTime);
    }
}