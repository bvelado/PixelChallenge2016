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
        
        DashEffect.SetActive(true);
        StartCoroutine(ActivateDashCollider());
        player.GetComponent<Rigidbody>().AddForce(player.transform.forward.normalized * dashForce);
        DashEffect.SetActive(false);
    }

    IEnumerator ActivateDashCollider()
    {
        print("Dash collider activated");
        dashCollider.SetActive(true);
        yield return new WaitForSeconds(dashDuration);
        print("Dash collider deactivated");
        dashCollider.SetActive(false);
    }
}
