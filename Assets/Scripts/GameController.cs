using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    #region Singleton pattern
    static GameController _instance;
    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion

    #region Public variables
    public List<CharacterData> characters = new List<CharacterData>();
    public Map map;

    public bool DEBUGSCENE = true;
    #endregion

    #region Private variables
    List<PlayerController> players = new List<PlayerController>();

    List<PlayerController> deadPlayers = new List<PlayerController>();

    Color[] playersColor =
    {
        new Color(255f/256f,145f/256f,10f/256f),
        new Color(0f,168f/256f,255f/256f),
        new Color(228f/256f,0f,255f/256f),
        new Color(51f/256f,196f/256f,0f)
    };
    #endregion

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this);

        DOTween.Init();
    }


    void Start()
    {
        if (DEBUGSCENE)
            InitGame();
        else
            Destroy(gameObject);
#if !UNITY_EDITOR
            gameObject.SetActive(false);
#endif
    }


    /// <summary>
    /// Load Map data then initialize the players
    /// </summary>
    public void InitGame()
    {
        if (GameObject.FindGameObjectWithTag("MapData"))
        {
            map = GameObject.FindGameObjectWithTag("MapData").GetComponent<Map>();
        }

        players.Clear();

        foreach (CharacterData character in characters)
        {
            players.Add(Instantiate(character.characterModel).GetComponent<PlayerController>());
        }
        
        for (int i = 0; i < players.Count; i++)
        {
            players[i].Init(i + 1, playersColor[i]);
            players[i].Spawn(map.data.mapSpawnPoints[i].position);

            UIController.Instance.CreatePlayer(i, playersColor[i]);
            UIController.Instance.CreatePlayerCooldown(i, players[i].secondsToResetStamina, playersColor[i]);

            players[i].PlayerDied += OnPlayerDied;
        }

        GameObject[] targets = new GameObject[players.Count];

        for(int i = 0; i < players.Count; i++)
        {
            targets[i] = players[i].gameObject;
        }

        if(CameraController.Instance != null)
            CameraController.Instance.SetTargets(targets);
    }

    public void AddPlayer(CharacterData player)
    {
        players.Add(player.characterModel.GetComponent<PlayerController>());
    }

    public void SetMap(Map map)
    {
        this.map = map;
    }

    public void OnPlayerDied(PlayerController player)
    {
        if (player.lives > 0)
        {
            player.Spawn(map.data.mapSpawnPoints[Random.Range(0, map.data.mapSpawnPoints.Length)].position);
        }
        else
        {
            print(player.name + "died");

            players.Remove(player);
            deadPlayers.Add(player);

            player.PlayerDied -= OnPlayerDied;
        }

        if (players.Count < 2)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void OnLevelWasLoaded(int level)
    {
        if(level != 0 && level != 1)
            InitGame();
    }


}
