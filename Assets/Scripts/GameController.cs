using UnityEngine;
using System.Collections.Generic;

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
    #endregion

    #region Private variables
    List<PlayerController> players = new List<PlayerController>();

    List<PlayerController> deadPlayers = new List<PlayerController>();
    #endregion

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this);
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
        else
        {
            print("No map data found");
        }

        players.Clear();

        foreach (CharacterData character in characters)
        {
            players.Add(Instantiate(character.characterModel).GetComponent<PlayerController>());
        }

        print(players.Count);

        // DEBUG : Charger autant de players que necessaire , sortir la valeur en dur
        for (int i = 0; i < players.Count; i++)
        {
            players[i].Init(i + 1);
            players[i].Spawn(map.data.mapSpawnPoints[i].position);

            players[i].PlayerDied += OnPlayerDied;
        }
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
            player.Spawn(map.data.mapSpawnPoints[3].position);
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
            print("Game finished. " + players[0].name + " won.");
        }
    }

    public void OnLevelWasLoaded(int level)
    {
        if(level != 0)
            InitGame();
    }
}
