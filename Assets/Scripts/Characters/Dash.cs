using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class Dash : MonoBehaviour, ICharacterAction
{

    #region Public variables
    public float dashForce;
    public float dashDensity;
    public float dashDuration;
    public GameObject DashEffect;
    #endregion

    #region Private variables
    PlayerController player;
    float baseDensity;
    #endregion

    void Start()
    {
        player = GetComponent<PlayerController>();
        baseDensity = player.GetComponent<Rigidbody>().mass;
    }

    public void Execute()
    {
        //        print("Dash");
        DashEffect.SetActive(true);
        player.GetComponent<Rigidbody>().AddForce(player.transform.forward.normalized * dashForce);
        StartCoroutine(SetDashMassDuringSeconds(dashDuration));
        DashEffect.SetActive(false);



    }

    IEnumerator SetDashMassDuringSeconds(float seconds)
    {
        player.GetComponent<Rigidbody>().mass = dashDensity;
        yield return new WaitForSeconds(seconds);
        player.GetComponent<Rigidbody>().mass = baseDensity;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.GetComponent<Movable>())
        {
            collision.transform.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
