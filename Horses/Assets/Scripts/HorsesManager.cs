using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleJSON;
using TMPro;
using UnityEngine.UI;

public class HorsesManager : MonoBehaviour
{
    private List<Horse> m_horses = new();
    [SerializeField] UserProfile m_userProfile;
    Action<string> m_createHorsesCallback;
    // Start is called before the first frame update
    void OnEnable ()
    {
        if (m_createHorsesCallback == null)
        {
            m_createHorsesCallback = (jsonArray) =>
            {
                StartCoroutine(CreateHorsesRoutine(jsonArray));
            };
        }

        CreateHorses();
    }

    public void CreateHorses()
    {
        string userID = Main.instance.userInfo.userID;

        while (m_horses.Count > 0)
        {
            Horse h = m_horses[0];
            m_horses.RemoveAt(0);
            Destroy(h.gameObject);
        }
        m_userProfile.Refresh();

        StartCoroutine(Main.instance.web.GetHorseIDs(userID, m_createHorsesCallback));
    }

    IEnumerator CreateHorsesRoutine(string jsonArrayString)
    {
        //Parsing json array as an array.
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;

        if (jsonArray != null && jsonArray.Count > 0)
        {
            for (int i = 0; i < jsonArray.Count; i++)
            {
                bool isDone = false; //done downloading?
                string horseID = jsonArray[i].AsObject["horseID"];
                string id = jsonArray[i].AsObject["ID"];
                JSONObject horseInfoJSON = new JSONObject();

                Action<string> getHorseInfoCallback = (horseInfo) =>
                {
                    isDone = true;
                    JSONArray tempArray = JSON.Parse(horseInfo) as JSONArray;
                    horseInfoJSON = tempArray[0] as JSONObject;
                };

                StartCoroutine(Main.instance.web.GetHorse(horseID, getHorseInfoCallback));

                yield return new WaitUntil(() => isDone);

                GameObject horseGO = Instantiate(Resources.Load("Prefabs/Horse") as GameObject);
                Horse horse = horseGO.AddComponent<Horse>();
                horse.ID = id;
                horse.HorseID = id;
                m_horses.Add(horse);

                horseGO.transform.SetParent(transform);
                horseGO.transform.localScale = Vector3.one;
                horseGO.transform.localPosition = Vector3.zero;

                string color = horseInfoJSON["color"];

                //fill info
                //horseGO.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = color;
                horseGO.transform.GetChild(0).Find("Price").GetComponent<TextMeshProUGUI>().text = horseInfoJSON["price"];

                AnimationClip clip = Main.instance.horseHides.GetHorseClip(horseID);

                horseGO.GetComponent<Animator>().Play(clip.name);


                // Set Sell button
                horseGO.transform.GetChild(0).Find("Sell Button").GetComponent<Button>().onClick.AddListener(() =>
                {
                    string userHorseID = id;
                    string hID = horseID;
                    string userID = Main.instance.userInfo.userID;

                    StartCoroutine(Main.instance.web.SellHorse(userHorseID, hID, userID, horseGO));
                    m_userProfile.Refresh();
                });
            }
        }

        yield return null;
    }
}
