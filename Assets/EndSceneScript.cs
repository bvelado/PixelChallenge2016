using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndSceneScript : MonoBehaviour {

    static EndSceneScript _instance;
    public static EndSceneScript Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (debug)
            DisplayLastWinners(dummyList);
    }

    public Transform[] charactersPositions;

    public CharacterData[] dummyList;
    public bool debug = false;

    public void DisplayLastWinners(CharacterData[] data)
    {

        dummyList = data;

        for(int i = 0; i < data.Length; i++)
        {
            Transform go = Instantiate(data[i].characterWinAnimated.transform);
            go.transform.SetParent(charactersPositions[i]);
            go.transform.localPosition = Vector3.zero;
        }
    }

    public void Clear()
    {
        foreach(Transform t in charactersPositions)
        {
            for(int i =0; i < t.childCount; i++)
            {
                Destroy(t.GetChild(i).gameObject);
            }
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
