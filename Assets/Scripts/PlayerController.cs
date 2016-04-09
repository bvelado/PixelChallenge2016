using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public delegate void PlayerDiedEventHandler(PlayerController player);
    public event PlayerDiedEventHandler PlayerDied;
    
    #region Private variables
    Rigidbody rb;
    ICharacterAction action;

    float baseDrag;
    bool grounded;
    bool invincible = false;

    Vector3 movement;
    Vector3 velocity;

    float stamina;
    float maxStamina;
    float actionCost;

    public int lives;

    bool controllable = true;
    #endregion

    #region Public variables
    public int playerId;

    public float movementSpeed = 14f;
    public float maxVelocity = 5f;

    public float velocitySmoothLerp;

    public float secondsToResetStamina;
    public int maxActions;

    public float invincibleTime;

    public SpriteRenderer selectCircle;
    #endregion

	void Update () {

        if(invincible && grounded)
        {
            rb.useGravity = false;
            GetComponent<CapsuleCollider>().enabled = false;
        } else {
            rb.useGravity = true;
            GetComponent<CapsuleCollider>().enabled = true;
        }

        //Debug.DrawRay(rb.position, Vector3.down * 4f, Color.red);
        if (Physics.Raycast(rb.position, Vector3.down, 0.3f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

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
        velocity = rb.velocity;

        if (Input.GetAxis("P" + playerId + "Horizontal") != 0f || Input.GetAxis("P" + playerId + "Vertical") != 0f && grounded)
        {
            movement.x = Input.GetAxis("P" + playerId + "Horizontal");
            movement.z = Input.GetAxis("P" + playerId + "Vertical");

            // Anim
			gameObject.GetComponent<Animation>().CrossFade("walk");
            
            transform.LookAt(transform.position + movement);

            rb.AddForce(movement * movementSpeed);
        }
		else {
            // Anim
			gameObject.GetComponent<Animation>().CrossFade("idle");
		}

        // Limite la velocity

        if (Mathf.Abs(velocity.x) > maxVelocity)
            velocity.x = Mathf.Lerp(velocity.x, Mathf.Sign(velocity.x) * maxVelocity, Time.deltaTime * velocitySmoothLerp);
        if (Mathf.Abs(velocity.z) > maxVelocity)
            velocity.z = Mathf.Lerp( velocity.z, Mathf.Sign(velocity.z) * maxVelocity, Time.deltaTime * velocitySmoothLerp);

        if(!grounded)
            velocity.y = Physics.gravity.y;

        if(controllable)
            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * velocitySmoothLerp);

        rb.velocity = velocity;

        UIController.Instance.UpdatePlayerStamina(playerId, stamina / maxStamina);
    }

    public void Die()
    {
        lives--;
        rb.velocity = Vector3.zero;
        rb.position = Vector3.zero;

        UIController.Instance.SetPlayerLives(playerId, lives);

        gameObject.SetActive(false);
        PlayerDied(this);

        CameraController.Instance.Shake();
    }

    public void Init(int playerId, Color color)
    {
        rb = GetComponent<Rigidbody>();
        action = GetComponent<ICharacterAction>();

        baseDrag = rb.drag;

        // Init 
        stamina = maxStamina = secondsToResetStamina;
        actionCost = maxStamina / maxActions;
        lives = 3;

        this.playerId = playerId;

        selectCircle.color = color;
    }

    public void Spawn(Vector3 spawnPos)
    {
        transform.position = spawnPos;
        rb.velocity = Vector3.zero;
        gameObject.SetActive(true);
        SetInvincible();
    }

    public void SetUncontrollable(float seconds)
    {
        StartCoroutine(Uncontrollable(seconds));
    }

    public void SetInvincible()
    {
        StartCoroutine(Invincible(invincibleTime));
    }

    IEnumerator Uncontrollable(float seconds)
    {
        controllable = false;
        yield return new WaitForSeconds(seconds);
        controllable = true;
    }

    IEnumerator Invincible(float seconds)
    {
        invincible = true;
        StartCoroutine(Clignote(seconds));
        yield return new WaitForSeconds(seconds);
        invincible = false;
    }

    IEnumerator Clignote(float seconds)
    {
        float elapsedTime = 0f;
        while(elapsedTime < seconds)
        {
            transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
            transform.GetChild(1).gameObject.SetActive(!transform.GetChild(1).gameObject.activeSelf);
            elapsedTime += 0.3f;
            yield return new WaitForSeconds(0.1f);
        }
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
    }
}
