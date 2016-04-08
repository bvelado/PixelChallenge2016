using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Explosion : MonoBehaviour, ICharacterAction
{
    #region Public variables
    public SphereCollider range;

    public float explosionRadius;
    public float maxExplosionForce;
    #endregion

    #region Private variables
    List<Movable> movablesInRange = new List<Movable>();
    #endregion

    void Start()
    {
        range.radius = explosionRadius;
        range.isTrigger = true;
    }

    public void Execute()
    {
        print("Explosion");
        foreach (Movable movable in movablesInRange)
        {
            float force = (explosionRadius - Vector3.Distance(movable.transform.position, transform.position)) / explosionRadius;
            movable.GetComponent<Rigidbody>().AddForce((movable.transform.position - transform.position).normalized * force * maxExplosionForce);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //print(other.name + " entered in range of " + name);
        if (other.GetComponent<Movable>() != null && !movablesInRange.Contains(other.GetComponent<Movable>()) && other != this)
            movablesInRange.Add(other.GetComponent<Movable>());
    }

    public void OnTriggerExit(Collider other)
    {
        //print(other.name + " left range of " + name);
        if (other.GetComponent<Movable>() != null && movablesInRange.Contains(other.GetComponent<Movable>()) && other != this)
            movablesInRange.Remove(other.GetComponent<Movable>());
    }
}
