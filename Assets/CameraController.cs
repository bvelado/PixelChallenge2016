using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    static CameraController _instance;
    public static CameraController Instance
    {
        get
        {
            return _instance;
        }
    } 

    public GameObject[] targets;
    public Vector3 cameraOffset;
    public float zoomFactor;

    float zoomValue;

    Vector3 center;

    Vector3 leftestPos, rightestPos;
	
    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

	void Update () {
        center = Vector3.zero;
        if(targets.Length > 0)
        {
            leftestPos = targets[0].transform.position;
            rightestPos = targets[0].transform.position;
        }
        

        foreach(GameObject target in targets)
        {
            center += target.transform.position;
            if (target.transform.position.x < leftestPos.x)
                leftestPos = target.transform.position;
            if (target.transform.position.x > rightestPos.x)
                rightestPos = target.transform.position;
        }

        if(targets.Length > 0)
            center = center / targets.Length;

        zoomValue = -Mathf.Abs(rightestPos.x - leftestPos.x);

        transform.position = center + cameraOffset + (zoomValue * transform.forward * zoomFactor);
	}

    public void SetTargets(GameObject[] targets)
    {
        this.targets = targets;
    }
}
