using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
    #region Private variables
    Rigidbody rb;
    ICharacterAction action;

    Vector3 movement;
    Vector3 velocity;

    float stamina;
    float maxStamina;
    float actionCost;
    #endregion

    #region Public variables
    public int playerId;

    public float movementSpeed = 14f;
    public float maxVelocity = 5f;

    public float velocitySmoothLerp;

    public float secondsToResetStamina;
    public int maxActions;
    #endregion

    void Start () {
        rb = GetComponent<Rigidbody>();
        action = GetComponent<ICharacterAction>();

        stamina = maxStamina = secondsToResetStamina;
        actionCost = maxStamina / maxActions;
    }
	
	void Update () {
        // Action

        stamina = Mathf.Clamp(Time.deltaTime + stamina, 0, maxStamina);

        if (Input.GetButtonDown("P" + playerId + "Action") && stamina >= actionCost)
        {
            action.Execute();
            stamina -= actionCost;
        }

        // Mouvement

        movement = Vector3.zero;

        if (Input.GetAxis("P" + playerId + "Horizontal") != 0f || Input.GetAxis("P" + playerId + "Vertical") != 0f)
        {
            movement.x = Input.GetAxis("P" + playerId + "Horizontal");
            movement.z = Input.GetAxis("P" + playerId + "Vertical");

            transform.LookAt(transform.position + movement);
        }

        rb.AddForce(movement * movementSpeed);

        // Limite la velocity

        velocity = rb.velocity;

        if (Mathf.Abs(velocity.x) > maxVelocity)
            velocity.x = Mathf.Lerp(velocity.x, Mathf.Sign(velocity.x) * maxVelocity, Time.deltaTime * velocitySmoothLerp);
        if (Mathf.Abs(velocity.z) > maxVelocity)
            velocity.z = Mathf.Lerp( velocity.z, Mathf.Sign(velocity.z) * maxVelocity, Time.deltaTime * velocitySmoothLerp);

        rb.velocity = velocity;
	}
}
