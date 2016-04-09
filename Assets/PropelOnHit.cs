using UnityEngine;
using System.Collections;

public class PropelOnHit : MonoBehaviour
{
    public float propelForceObjects;
    public float propelForcePlayers;
    public float disableDuration;

    public void OnTriggerEnter(Collider collider)
    {
        print("Ouch");
        if (collider.GetComponent<PlayerController>())
        {
            print("Ouch player");
            collider.GetComponent<PlayerController>().SetUncontrollable(disableDuration);
            collider.GetComponent<Rigidbody>().AddForce(transform.forward * propelForcePlayers);
        } else if (collider.GetComponent<Movable>())
        {
            print("Ouch charly");
            collider.GetComponent<Rigidbody>().AddForce(transform.forward * propelForceObjects);
        }
    }
}
