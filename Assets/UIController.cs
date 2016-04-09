﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

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

    public Text[] playerLives;
    public RectTransform _containerCooldown;
    public Transform cooldownUIprefab;
    List<Image> playerCooldowns = new List<Image>();

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
    }

    public void CreatePlayerCooldown(int playerId, float maxValue, Color color)
    {
        PlayerCooldownUI playerCooldown = new PlayerCooldownUI();
        playerCooldown.value = playerCooldown.maxValue = 1f;
        cooldownsData.Add(playerCooldown);

        Transform tr = Instantiate(cooldownUIprefab);
        tr.SetParent(_containerCooldown);
        tr.localScale = Vector3.one;
        tr.GetComponent<Image>().color = color;
        playerCooldowns.Add(tr.GetComponent<Image>());
    }

    public void UpdatePlayerStamina(int playerId, float value)
    {
        playerCooldowns[playerId-1].fillAmount = value;
    }
}