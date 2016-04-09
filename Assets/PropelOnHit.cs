using UnityEngine;
using System.Collections;

public class PropelOnHit : MonoBehaviour
{
    public float propelForceObjects;
    public float propelForcePlayers;
    public float disableDuration;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<PlayerController>())
        {
            collider.GetComponent<PlayerController>().SetUncontrollable(disableDuration);
            collider.GetComponent<Rigidbody>().AddForce(transform.forward * propelForcePlayers);
        } else if (collider.GetComponent<Movable>())
        {
            collider.GetComponent<Rigidbody>().AddForce(transform.forward * propelForceObjects);
        }
    }
}
