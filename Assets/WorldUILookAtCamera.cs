using UnityEngine;
using System.Collections;

public class WorldUILookAtCamera : MonoBehaviour {

    public Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    [ExecuteInEditMode]
	void Update () {
        transform.LookAt(cam.transform.position);
	}
}
