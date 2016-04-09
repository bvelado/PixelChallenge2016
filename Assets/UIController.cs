using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    static UIController _instance;
    public static UIController Instance
    {
        get
        {
            return _instance;
        }
    }

    public Text[] playerLives;

	void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public void SetPlayerLives(int playerId, int value)
    {
        playerLives[playerId - 1].text = value.ToString();
    }
}
