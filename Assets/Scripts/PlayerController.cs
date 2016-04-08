using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public delegate void PlayerDiedEventHandler(PlayerController player);
    public event PlayerDiedEventHandler PlayerDied;
    
    #region Private variables
    Rigidbody rb;
    ICharacterAction action;

    Vector3 movement;
    Vector3 velocity;

    float stamina;
    float maxStamina;
    float actionCost;

    public int lives;
    #endregion

    #region Public variables
    public int playerId;

    public float movementSpeed = 14f;
    public float maxVelocity = 5f;

    public float velocitySmoothLerp;

    public float secondsToResetStamina;
    public int maxActions;
    #endregion

	void Update () {
        // Action

        stamina = Mathf.Clamp(Time.deltaTime + stamina, 0, maxStamina);

        if (Input.GetButtonDown("P" + playerId + "Action") && stamina >= actionCost)
        {
			gameObject.GetComponent<Animation>().Play("action");
			action.Execute();
            stamina -= actionCost;
        }

        // Mouvement

        movement = Vector3.zero;

        if (Input.GetAxis("P" + playerId + "Horizontal") != 0f || Input.GetAxis("P" + playerId + "Vertical") != 0f)
        {
            movement.x = Input.GetAxis("P" + playerId + "Horizontal");
            movement.z = Input.GetAxis("P" + playerId + "Vertical");

            // Anim
			gameObject.GetComponent<Animation>().CrossFade("walk");


            transform.LookAt(transform.position + movement);
        }
		else {
            // Anim
			gameObject.GetComponent<Animation>().CrossFade("idle");


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

    public void Die()
    {
        lives--;
        rb.velocity = Vector3.zero;
        rb.position = Vector3.zero;
        
        gameObject.SetActive(false);
        PlayerDied(this);
    }

    public void Init(int playerId)
    {
        rb = GetComponent<Rigidbody>();
        action = GetComponent<ICharacterAction>();

        // Init 
        stamina = maxStamina = secondsToResetStamina;
        actionCost = maxStamina / maxActions;
        lives = 3;

        this.playerId = playerId;
    }

    public void Spawn(Vector3 spawnPos)
    {
        transform.position = spawnPos;
        rb.velocity = Vector3.zero;
        gameObject.SetActive(true);
    }
}
