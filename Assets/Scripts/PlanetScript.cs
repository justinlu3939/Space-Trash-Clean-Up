using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour
{
    //make the planet target itself
    public Transform target;
    public float spinSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        //make the planet rotate around its axis
        transform.RotateAround(target.position, Vector3.up, spinSpeed * Time.deltaTime);
    }
}
