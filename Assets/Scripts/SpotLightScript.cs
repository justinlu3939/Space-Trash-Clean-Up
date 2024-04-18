using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightScript : MonoBehaviour
{
    public float maxRotationAngle = 45f; // Maximum rotation angle
    public float rotationSpeed = 1f; // Speed of rotation

    private float initialRotation;
    // Start is called before the first frame update
    void Start()
    {
        // Save the initial rotation
        initialRotation = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new rotation angle
        float rotationAngle = maxRotationAngle * Mathf.Sin(Time.time * rotationSpeed);

        // Apply the rotation to the spotlight
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, initialRotation + rotationAngle, transform.eulerAngles.z);
    }
}
