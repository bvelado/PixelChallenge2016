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
    public Transform[] spawnPoints;
    public int nbPlayers;

    /// <summary>
    /// Characters prefabs are loaded into that list to initiate the game.
    /// </summary>
    List<GameObject> characters = new List<GameObject>();
    #endregion

    #region Private variables
    List<int> usedSpawnPoints = new List<int>();
    /// <summary>
    /// Actual playing characters
    /// </summary>
    List<PlayerController> players = new List<PlayerController>();
    #endregion

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    void Start () {
            
	}

    public void AddCrusher()
    {
        characters.Add(Resources.Load("Prefabs/Characters/Crusher") as GameObject);
    }

    public void AddDasher()
    {
        characters.Add(Resources.Load("Prefabs/Characters/Dasher") as GameObject);
    }

    public void AddRepulser()
    {
        characters.Add(Resources.Load("Prefabs/Characters/Repulser") as GameObject);
    }

    /// <summary>
    /// Reset the game base on the loaded characters list
    /// </summary>
    public void Reset()
    {
        ClearCharacters();

        for(int i = 0; i < characters.Count; i++)
        {
            SpawnCharacter(i);
        }

        usedSpawnPoints.Clear();
    }

    /// <summary>
    /// Clear all characters
    /// </summary>
    public void ClearCharacters(bool resetCharacterList = false)
    {
        foreach (PlayerController character in players)
        {
            Destroy(character.gameObject);
        }
        players.Clear();

        if(resetCharacterList)
            characters.Clear();
    }

    /// <summary>
    /// Remove the character from the players list
    /// </summary>
    /// <param name="character"></param>
    void RemoveCharacter(PlayerController character)
    {
        Destroy(character.gameObject);
        players.Remove(character);
    }



    void SpawnCharacter(int playerId)
    {
        PlayerController player = Instantiate(characters[playerId]).GetComponent<PlayerController>();
        player.Spawn();

        bool hasSpawn = false;

        while(!hasSpawn)
        {
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            if (!usedSpawnPoints.Contains(randomSpawnIndex))
            {
                player.transform.position = spawnPoints[randomSpawnIndex].position;
                usedSpawnPoints.Add(randomSpawnIndex);
                hasSpawn = true;
            } else if(usedSpawnPoints.Count == spawnPoints.Length)
            {
                Debug.LogWarning("Pas assez de spawn points par rapport au nombre de joueur. (" + spawnPoints.Length + " spawns pour " + nbPlayers + " joueurs).");
                hasSpawn = true;
            }
        }
        
        player.playerId = playerId + 1;

        players.Add(player);
    }
}
