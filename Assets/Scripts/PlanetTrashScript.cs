using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetTrashScript : MonoBehaviour
{
    //target the planet
    public Transform planetTarget;

    //rotation speed around the planet
    public float rotationSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        //RotateAround method
        transform.RotateAround(planetTarget.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
