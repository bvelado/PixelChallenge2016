using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button playButton;
    AsyncOperation asyncSceneLoad;

    bool _canPlay = false;
    bool canPlay
    {
        get { return _canPlay; }
        set
        {
            playButton.interactable = value;
            _canPlay = value;
        }
    }

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

        if (GameController.Instance.characters.Count >= 2)
        {
            canPlay = true;
        }
    }

    public void AddRepulser()
    {
        GameController.Instance.characters.Add(repulserData);
        Transform charUI = Instantiate(repulserData.characterPortraitSprite);
        charUI.SetParent(characterUIContainer);
        charUI.localScale = Vector3.one;

        charUI.GetComponent<RectTransform>().localPosition = Vector3.zero;
        charactersUI.Add(charUI.gameObject);

        if (GameController.Instance.characters.Count >= 2)
        {
            canPlay = true;
        }
    }

    public void AddDasher()
    {
        GameController.Instance.characters.Add(dasherData);
        Transform charUI = Instantiate(dasherData.characterPortraitSprite);
        charUI.SetParent(characterUIContainer);
        charUI.localScale = Vector3.one;

        charUI.GetComponent<RectTransform>().localPosition = Vector3.zero;
        charactersUI.Add(charUI.gameObject);

        if (GameController.Instance.characters.Count >= 2)
        {
            canPlay = true;
        }
    }

    public void Clear()
    {
        foreach (GameObject go in charactersUI)
        {
            Destroy(go);
        }
        charactersUI.Clear();
        GameController.Instance.characters.Clear();

        canPlay = false;
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void OnLevelWasLoaded(int level)
    {
        charactersUI.Clear();
        canPlay = false;
        GameController.Instance.characters.Clear();
    }
}
