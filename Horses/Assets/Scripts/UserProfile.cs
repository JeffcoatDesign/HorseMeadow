using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using TMPro;
using UnityEngine;

public class UserProfile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_userNameText;
    [SerializeField] private TextMeshProUGUI m_moneyText;

    private void OnEnable()
    {
        m_userNameText.text = Main.instance.userInfo.username;
        Refresh();
    }

    public void Refresh()
    {
        StartCoroutine(Main.instance.web.GetCoins(Main.instance.userInfo.userID, RecieveUserCoins));
    }

    private void RecieveUserCoins(int coins)
    {
        m_moneyText.text = "Money: " + coins;
    }
}
