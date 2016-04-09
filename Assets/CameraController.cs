using UnityEngine;
using System.Collections;
using DG.Tweening;

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

    public float camSpeed;

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

        zoomValue = rightestPos.x - leftestPos.x;

        center += -zoomValue * zoomFactor * transform.forward;

        transform.position = Vector3.Lerp(transform.position, center+cameraOffset, camSpeed * Time.deltaTime);
    }

    public void SetTargets(GameObject[] targets)
    {
        this.targets = targets;
    }

    public void Shake()
    {
        if (DOTween.Complete(transform) > 0)
            DOTween.Clear(transform);
        
        transform.DOShakePosition(.4f);
    }
}
