using UnityEngine;
using System.Collections;

public enum PlayerControls
{
    Player1, Player2
}

public class PlayerController : MonoBehaviour {
    
    #region Private variables
    Rigidbody rb;
    ICharacterAction action;

    float inputHorizontalAxis;
    float inputVerticalAxis;
    bool inputAction;

    Vector3 movement;
    Vector3 velocity;
    #endregion

    #region Public variables
    public PlayerControls controls;

    public float movementSpeed = 14f;
    public float maxVelocity = 5f;

    public float velocitySmoothLerp;
    #endregion

    void Start () {
        rb = GetComponent<Rigidbody>();
        action = GetComponent<ICharacterAction>();
	}
	
	void Update () {

        if (controls == PlayerControls.Player1)
        {
            inputHorizontalAxis = Input.GetAxis("Horizontal");
            inputVerticalAxis = Input.GetAxis("Vertical");
            inputAction = Input.GetButtonDown("Fire1");
        }

        // Action

        if(inputAction)
        {
            action.Execute();
        }

        // Mouvement

        movement = Vector3.zero;

        if (inputHorizontalAxis != 0f || inputVerticalAxis != 0f)
        {
            movement.x = inputHorizontalAxis;
            movement.z = inputVerticalAxis;

            transform.LookAt(transform.position + movement);
        }

        rb.AddForce(movement * movementSpeed);

        // Limite la velocity

        velocity = rb.velocity;

        if (Mathf.Abs(velocity.x) > maxVelocity)
            velocity.x = Mathf.Lerp(velocity.x, Mathf.Sign(velocity.x) * maxVelocity, Time.deltaTime * velocitySmoothLerp);
        if (Mathf.Abs(velocity.z) > maxVelocity)
            velocity.z = Mathf.Lerp( velocity.z, Mathf.Sign(velocity.z) * maxVelocity, Time.deltaTime * velocitySmoothLerp);

        print(velocity);

        rb.velocity = velocity;
	}
}
