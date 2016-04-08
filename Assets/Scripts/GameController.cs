using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    #region Singleton pattern
    GameController _instance;
    public GameController Instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion

    #region Public variables
    public List<PlayerController> players = new List<PlayerController>();
    public Map map;
    #endregion

    #region Private variables
    #endregion

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        // ONLY FOR DEBUG
        InitGame();
    }

    /// <summary>
    /// Load Map data then initialize the players
    /// </summary>
    public void InitGame()
    {
        // TODO : Changer le systeme pour load la map data
        SetMap(GameObject.Find("MapData").GetComponent<Map>());

        // DEBUG : Charger autant de players que necessaire , sortir la valeur en dur
        for(int i = 1; i < 2; i++)
        {
            //GameObject go = Instantiate(players[i]);
            players[i].Init(i);
            players[i].Spawn(map.data.mapSpawnPoints[i]);

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
        if(player.lives > 0)
        {
            player.Spawn(map.data.mapSpawnPoints[Random.Range(0, map.data.mapSpawnPoints.Length)]);
        }
    }
}
