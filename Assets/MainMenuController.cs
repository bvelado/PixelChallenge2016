using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour {

    public Transform crusherUIprefab, dasherUIprefab, repulserUIprefab;

    public Transform characterContainer;

    List<GameObject> characters;
    List<GameObject> charactersUI;

	void Start () {
	    
	}
	
	void Update () {
	    
	}

    public void AddCrusher()
    {
        characters.Add(Resources.Load("Prefabs/Characters/Crusher") as GameObject);
        Transform charUI = Instantiate(Resources.Load("Prefabs/UI/Crusher") as Transform);
        charUI.SetParent(characterContainer);
        charactersUI.Add(charUI.gameObject);
    }

    public void AddRepulser()
    {
        characters.Add(Resources.Load("Prefabs/Characters/Repulser") as GameObject);
        Transform charUI = Instantiate(Resources.Load("Prefabs/UI/Repulser") as Transform);
        charUI.SetParent(characterContainer);
        charactersUI.Add(charUI.gameObject);
    }

    public void AddDasher()
    {
        characters.Add(Resources.Load("Prefabs/Characters/Dasher") as GameObject);
        Transform charUI = Instantiate(Resources.Load("Prefabs/UI/Dasher") as Transform);
        charUI.SetParent(characterContainer);
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
