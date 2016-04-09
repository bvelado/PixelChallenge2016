using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public struct PlayerCooldownUI
{
    public float value;
    public float maxValue;
    public int stacks;
    public int maxStacks;
    public float actionCost;
}

public class UIController : MonoBehaviour {

    static UIController _instance;
    public static UIController Instance
    {
        get
        {
            return _instance;
        }
    }

    public List<Text> playerLives = new List<Text>();
    public RectTransform _containerCooldown;
    public Transform cooldownUIprefab;
    public RectTransform _containerPlayer;
    public Transform playerUIprefab;

    List<RectTransform> playerCooldowns = new List<RectTransform>();
    List<RectTransform> playerBlocks = new List<RectTransform>();

    List<PlayerCooldownUI> cooldownsData = new List<PlayerCooldownUI>();

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

        Sequence seq = DOTween.Sequence();

        seq.Append(playerCooldowns[playerId - 1].GetChild(1).transform.DOScale(Vector3.one * 2.4f, .4f))
            .Append(playerCooldowns[playerId - 1].GetChild(1).transform.DOScale(Vector3.one, .4f));

        seq.Play();

    }

    public void CreatePlayerCooldown(int playerId, float maxValue, Color color)
    {
        PlayerCooldownUI playerCooldown = new PlayerCooldownUI();
        playerCooldown.value = playerCooldown.maxValue = 1f;
        cooldownsData.Add(playerCooldown);

        Transform tr = Instantiate(cooldownUIprefab);
        tr.SetParent(_containerCooldown);
        tr.localScale = Vector3.one;
        tr.GetChild(0).GetComponent<Image>().color = color;
        tr.GetChild(1).GetComponent<Image>().color = color;
        playerLives.Add(tr.GetChild(1).GetComponentInChildren<Text>());
        playerCooldowns.Add(tr.GetComponent<RectTransform>());
    }

    public void CreatePlayer(int playerId, Color color)
    {
        Transform tr = Instantiate(playerUIprefab);
        tr.SetParent(_containerPlayer);
        tr.localScale = Vector3.one;
        tr.GetComponentInChildren<Text>().text = "Player " + (playerId + 1);
        tr.GetComponentInChildren<Text>().color = color;

        playerBlocks.Add(tr.GetComponent<RectTransform>());
    }

    public void UpdatePlayerStamina(int playerId, float value)
    {
        playerCooldowns[playerId - 1].GetChild(0).GetComponent<Image>().fillAmount = value;
    }
}
