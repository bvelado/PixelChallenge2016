using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class Dash : MonoBehaviour, ICharacterAction {

    #region Public variables
    public float dashForce;
    public float dashDensity;
    public float dashDuration;
    #endregion

    #region Private variables
    PlayerController player;
    float baseDensity;
    #endregion

    void Start () {
        player = GetComponent<PlayerController>();
        baseDensity = player.GetComponent<Rigidbody>().mass;
	}
	
	public void Execute()
    {
        print("Dash");
        player.GetComponent<Rigidbody>().AddForce(player.transform.forward.normalized * dashForce);
        StartCoroutine(SetDashMassDuringSeconds(dashDuration));
    }

    IEnumerator SetDashMassDuringSeconds(float seconds)
    {
        player.GetComponent<Rigidbody>().mass = dashDensity;
        yield return new WaitForSeconds(seconds);
        player.GetComponent<Rigidbody>().mass = baseDensity;
    }
}
