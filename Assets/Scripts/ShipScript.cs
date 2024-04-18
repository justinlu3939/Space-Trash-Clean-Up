using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShipScript : MonoBehaviour
{
    //create the rigid body for the ship
    private Rigidbody rb;
    public GameObject boostSprite;
    private float forwardSpeed = 10f; // Adjust this for desired forward speed
    private float rotationSpeed = 100f; // Adjust this for desired rotation speed
    private float returnRotationSpeed = 10f; // Speed for automatic rotation back

    private Quaternion initialRotation; // Store the initial rotation

    //this is the boost speed
    private float boost = 2f;
    //this scores the original speed to go back to when the boost isn't used
    private float originalSpeed;

    //this is for the score
    private float scoreCounter;
    public TextMeshProUGUI countText; //this is displayed to the Canvas

    //these variables are for the stamina bar
    public Slider staminaBar;
    public float maxStamina = 10f;

    //these variables are for the heatlh bar
    public Slider healthBar;
    public float maxHealth = 10f;

    //add joystick
    public Joystick joystick;

    //add boost button
    public Button boostButton;
    private bool isBoosting;

    //store original position for the world boundary
    public Vector3 originalPosition;

    private void Start()
    {
        //ship configurations
        rb = GetComponent<Rigidbody>();
        initialRotation = transform.rotation; // Store the initial rotation
        originalSpeed = forwardSpeed;

        //this sets the counter to zero and the function displays the score to the canvas.
        scoreCounter = 0;
        SetCountText();

        //stamina bar
        staminaBar.value = maxStamina;
        staminaBar.maxValue = maxStamina;

        //health bar
        healthBar.value = maxHealth;
        healthBar.maxValue = maxHealth;

        //set event trigger for boost so that the boost will only work when holding down button
        EventTrigger trigger = boostButton.gameObject.AddComponent<EventTrigger>();
        var pointerDown = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        pointerDown.callback.AddListener((data) => { isBoosting = true; });
        trigger.triggers.Add(pointerDown);

        //this code fixes the joystick up and down rotation movement
        var pointerUp = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        pointerUp.callback.AddListener((data) => { isBoosting = false; });
        trigger.triggers.Add(pointerUp);

        originalPosition = transform.position;

        //resume the time back to normal speed
        Time.timeScale = 1;
    }

    private void FixedUpdate()
    {
        // Move the ship forward automatically
        Vector3 forwardMovement = transform.forward * forwardSpeed;
        rb.velocity = forwardMovement; // Set velocity directly for linear movement

        // Rotate the ship using the joystick
        float rotationInput = joystick.Horizontal;
        // float rotationInput = Input.GetAxis("Horizontal");
        float rotationAmount = rotationInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotationAmount);

        // Rotate the ship upwards and downwards using the joystick
        float verticalRotationInput = joystick.Vertical;
        // float verticalRotationInput = Input.GetAxis("Vertical");
        float verticalRotationAmount = verticalRotationInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.left, verticalRotationAmount); // Use Vector3.left for vertical rotation

        //if the space bar or boost button is being pressed and there is still stamina
        if((Input.GetKey("space") || isBoosting) && staminaBar.value > 0)
        {
            boostingTime();
        }
        else
        {
            forwardSpeed = originalSpeed;
            boostSprite.SetActive(false);
            //let the stamina bar recharge.
            if(staminaBar.value <= maxStamina)
            {
                staminaBar.value += (Time.deltaTime / 2);
            }
        }
        // Automatic rotation back to initial orientation
        if (!Input.GetKey("a") && !Input.GetKey("d") && !Input.GetKey("w") && !Input.GetKey("s")) //Mathf.Abs(joystick.Horizontal) < 0.1f && Mathf.Abs(joystick.Vertical) < 0.1f
        {
            // Interpolate towards the initial rotation
            Quaternion targetRotation = Quaternion.LookRotation(forwardMovement.normalized, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, returnRotationSpeed * Time.deltaTime);
        }
    
        //create the sphere cast for collision detection
        float sphereCastRadius = 1f;
        float sphereCastLength = 5f;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.SphereCast(ray, sphereCastRadius, out hit, sphereCastLength))
        {
            // If the raycast hits an object with the tag "Trash"
            if (hit.collider.gameObject.CompareTag("Trash"))
            {
                // Log a message or perform an action
                Debug.Log("The ship has collided with trash!");
                hit.collider.gameObject.SetActive(false);
                scoreCounter += 15;
                SetCountText();
            }
            //add another if statement here that allows for the collision between moons, satellites and planets
            if(hit.collider.gameObject.CompareTag("ObjectCollision"))
            {
                Debug.Log("Collided with object. lose health");
                healthBar.value -= 2;
                scoreCounter -= 1;
            }
            //add another if statement here that detects if the ship has left the boundaries.
            if(hit.collider.gameObject.CompareTag("WorldBoundary"))
            {
                Debug.Log("Collided with boundary. Returning back to original.");
                //return back to original position and rotation
                transform.position = originalPosition;
                transform.rotation = initialRotation;
                //reduce score for violation
                scoreCounter -= 10;
                SetCountText();
            }
        }
    }

    //sets the score and displays it
    void SetCountText() 
   {
       countText.text =  "Score: " + scoreCounter.ToString();
   }

    //boost the ship and decrease the stamina bar
   public void boostingTime()
   {
        forwardSpeed += boost;
        boostSprite.SetActive(true);
        //decrease the stamina bar
        if(staminaBar.value > 0)
        {
            staminaBar.value -= (Time.deltaTime * 2);
        }
   }
}
