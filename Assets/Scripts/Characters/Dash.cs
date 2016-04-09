using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class Dash : MonoBehaviour, ICharacterAction
{

    #region Public variables
    public float dashForce;
    public float dashDensity;
    public float dashDuration;
	public GameObject effect;
    public GameObject dashCollider;
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
        
        StartCoroutine(ActivateDashCollider());
        player.GetComponent<Rigidbody>().AddForce(player.transform.forward.normalized * dashForce);
    }

    IEnumerator ActivateDashCollider()
    {
		effect.SetActive(true);

        print("Dash collider activated");
        dashCollider.SetActive(true);
        yield return new WaitForSeconds(dashDuration);
        print("Dash collider deactivated");
        dashCollider.SetActive(false);
		effect.SetActive(false);

    }
}
