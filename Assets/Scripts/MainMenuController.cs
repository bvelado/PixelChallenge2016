using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    AsyncOperation asyncSceneLoad;

//    public Transform crusherUIprefab, dasherUIprefab, repulserUIprefab;

    public Transform characterUIContainer;

    List<GameObject> charactersUI = new List<GameObject>();

    public CharacterData crusherData, dasherData, repulserData;

    public void AddCrusher()
    {
        GameController.Instance.characters.Add(crusherData);
		Transform charUI = Instantiate(crusherData.characterPortraitSprite);
        charUI.SetParent(characterUIContainer);
        charUI.localScale = Vector3.one;

        charUI.GetComponent<RectTransform>().localPosition = Vector3.zero;
        charactersUI.Add(charUI.gameObject);
    }

    public void AddRepulser()
    {
        GameController.Instance.characters.Add(repulserData);
		Transform charUI = Instantiate(repulserData.characterPortraitSprite);
        charUI.SetParent(characterUIContainer);
        charUI.localScale = Vector3.one;

        charUI.GetComponent<RectTransform>().localPosition = Vector3.zero;
        charactersUI.Add(charUI.gameObject);
    }

    public void AddDasher()
    {
        GameController.Instance.characters.Add(dasherData);
		Transform charUI = Instantiate(dasherData.characterPortraitSprite);
        charUI.SetParent(characterUIContainer);
        charUI.localScale = Vector3.one;

        charUI.GetComponent<RectTransform>().localPosition = Vector3.zero;
        charactersUI.Add(charUI.gameObject);
    }

    public void Clear()
    {
        foreach (GameObject go in charactersUI)
        {
            Destroy(go);
        }
        charactersUI.Clear();
        GameController.Instance.characters.Clear();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
