using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour {

    public Transform crusherUIprefab, dasherUIprefab, repulserUIprefab;

    public Transform characterContainer;

    List<PlayerController> characters = new List<PlayerController>();
    List<GameObject> charactersUI = new List<GameObject>();

	void Start () {
	    
	}
	
	void Update () {
	    
	}

    public void AddCrusher()
    {
        characters.Add(Resources.Load("Prefabs/Characters/Crusher") as PlayerController);
        Transform charUI = Instantiate(crusherUIprefab).transform;
        charUI.SetParent(characterContainer);
        charUI.localScale = Vector3.one;

        charUI.GetComponent<RectTransform>().localPosition = Vector3.zero;
        charactersUI.Add(charUI.gameObject);
    }

    public void AddRepulser()
    {
        characters.Add(Resources.Load("Prefabs/Characters/Repulser") as PlayerController);
        Transform charUI = Instantiate(repulserUIprefab).transform;
        charUI.SetParent(characterContainer);
        charUI.localScale = Vector3.one;

        charUI.GetComponent<RectTransform>().localPosition = Vector3.zero;
        charactersUI.Add(charUI.gameObject);
    }

    public void AddDasher()
    {
        characters.Add(Resources.Load("Prefabs/Characters/Dasher") as PlayerController);
        Transform charUI = Instantiate(dasherUIprefab).transform;
        charUI.SetParent(characterContainer);
        charUI.localScale = Vector3.one;

        charUI.GetComponent<RectTransform>().localPosition = Vector3.zero;
        charactersUI.Add(charUI.gameObject);
    }

    public void Clear()
    {
        foreach(GameObject go in charactersUI)
        {
            Destroy(go);
        }
        charactersUI.Clear();
        characters.Clear();
    }
}
