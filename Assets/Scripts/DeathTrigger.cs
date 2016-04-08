using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
            other.GetComponent<PlayerController>().Die();
    }
}
