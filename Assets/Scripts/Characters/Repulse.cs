using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Repulse : MonoBehaviour, ICharacterAction {

    #region Public variables
    public BoxCollider range;
	public GameObject RepulseEffect;
    public float repulseRange;
    public float maxRepulseForce;
    #endregion

    #region Private variables
    List<Movable> movablesInRange = new List<Movable>();
    #endregion

    public void Execute()
    {
        foreach (Movable movable in movablesInRange)
        {
			RepulseEffect.SetActive(true);
            float force = (repulseRange - Vector3.Distance(movable.transform.position, transform.position)) / repulseRange;
            movable.GetComponent<Rigidbody>().AddForce((movable.transform.position - transform.position).normalized * force * maxRepulseForce);
        }
    }

    void Start () {
        range.isTrigger = true;
        Vector3 newRange = range.size;
        newRange.z = repulseRange;
        range.size = newRange;
        newRange = Vector3.zero;
        newRange.z = repulseRange / 2f;
        range.center = newRange;
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
