using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneAfterAnimation : MonoBehaviour {

    public float animationDuration;
    public int nextSceneIndex;

    bool overrideLaunch = false;

    void Start()
    {
        StartCoroutine(LaunchNextSceneAfter());
        StartCoroutine(OverrideLaunch());
    }

    void Update () {
	    if(Input.GetButton("P1Action"))
        {
            overrideLaunch = true;
        }
	}

    void LaunchNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }

    IEnumerator LaunchNextSceneAfter()
    {
        yield return new WaitForSeconds(animationDuration);
        if(!overrideLaunch)
            LaunchNextScene();
    }

    IEnumerator OverrideLaunch()
    {
        while (overrideLaunch == false)
            yield return null;
        LaunchNextScene();
    }
}
