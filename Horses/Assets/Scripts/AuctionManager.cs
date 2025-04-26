using SimpleJSON;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AuctionManager : MonoBehaviour
{
    [SerializeField] private Transform m_horseParent;
    [SerializeField] private TMP_InputField m_inputField;
    [SerializeField] private TextMeshProUGUI m_resultText;
    [SerializeField] private UserProfile m_userProfile;
    [SerializeField] private float m_bidChance = 0.5f;

    private GameObject m_horseObject;
    private Horse m_horse;
    private int m_currentBid;
    private int m_horseValue;

    private void OnEnable()
    {
        if (m_horseObject != null)
        {
            Destroy(m_horseObject);
        }

        StartCoroutine(GetRandomHorse());
    }

    public void BuyHorse()
    {
        string horseID = m_horse.HorseID;
        string userID = Main.instance.userInfo.userID;
        string bidText = m_inputField.text;
        
        if(!int.TryParse(bidText, out int bid))
        {
            return;
        }
        if (bid <= m_currentBid)
        {
            return;
        }
        if (CounterBid(bid))
        {
            return;
        }

        m_currentBid = bid;
        m_resultText.text = "Winning bid: " + m_currentBid;

        Action<string> buyHorseCallback = (result) =>
        {
            if (result == "Success!")
            {
                m_userProfile.Refresh();
                //TODO show that it was a success
            }

            StartCoroutine(GetRandomHorse());
        };

        StartCoroutine(Main.instance.web.BuyHorse(bidText, horseID, userID, buyHorseCallback));
    }

    private bool CounterBid(int bid)
    {
        int difference = m_horseValue - bid;
        float skew = difference / m_horseValue;
        float chance = UnityEngine.Random.Range(0f, 1f);
        bool willBid = chance + skew >= m_bidChance;
        if (willBid) { 
            m_currentBid = bid + UnityEngine.Random.Range(10, 100);
            m_resultText.text = "New bid: " + m_currentBid;
        }
        return willBid;
    }

    private IEnumerator GetRandomHorse()
    {
        string id = UnityEngine.Random.Range(1, 16).ToString();
        m_resultText.text = "Opening bid: ";

        if (m_horseObject != null)
        {
            Destroy(m_horseObject);
        }

        bool isDone = false; //done downloading?
        JSONObject horseInfoJSON = new JSONObject();

        Action<string> getHorseInfoCallback = (horseInfo) =>
        {
            isDone = true;
            JSONArray tempArray = JSON.Parse(horseInfo) as JSONArray;
            horseInfoJSON = tempArray[0] as JSONObject;
        };

        StartCoroutine(Main.instance.web.GetHorse(id, getHorseInfoCallback));

        yield return new WaitUntil(() => isDone);

        m_horseObject = Instantiate(Resources.Load("Prefabs/AHorse") as GameObject);
        m_horse = m_horseObject.AddComponent<Horse>();
        m_horse.HorseID = id;
        m_horse.Price = horseInfoJSON["price"];
        m_horseObject.transform.SetParent(m_horseParent);
        m_horseObject.transform.localScale = Vector3.one;
        m_horseObject.transform.localPosition = Vector3.zero;

        m_horseValue = int.Parse(m_horse.Price);
        m_currentBid = Mathf.RoundToInt(UnityEngine.Random.Range(0.1f, 0.8f) * m_horseValue);
        m_resultText.text = "Opening bid: " + m_currentBid;

        //fill info
        //horseGO.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = color;
        //m_horseObject.transform.GetChild(0).Find("Price").GetComponent<TextMeshProUGUI>().text = "";

        var clip = Main.instance.horseHides.GetHorseClip(id);

        m_horseObject.transform.GetComponent<Animator>().Play(clip.name);


        // Hide Sell button
        //m_horseObject.transform.GetChild(0).Find("Sell Button").gameObject.SetActive(false);
    }
}
