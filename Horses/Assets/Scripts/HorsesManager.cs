using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleJSON;
using TMPro;
using UnityEngine.UI;
using UnityEngine.XR;

public class HorsesManager : MonoBehaviour
{
    Action<string> m_createHorsesCallback;
    // Start is called before the first frame update
    void Start()
    {
        m_createHorsesCallback = (jsonArray) => { 
            StartCoroutine(CreateItemsRoutine(jsonArray));
        };

        CreateHorses();
    }

    public void CreateHorses()
    {
        string userID = Main.instance.userInfo.userID;
        StartCoroutine(Main.instance.web.GetHorseIDs(userID, m_createHorsesCallback));
    }

    IEnumerator CreateItemsRoutine(string jsonArrayString)
    {
        //Parsing json array as an array.
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;

        for (int i = 0; i < jsonArray.Count; i++) {
            bool isDone = false; //done downloading?
            string itemID = jsonArray[i].AsObject["itemID"];
            string id = jsonArray[i].AsObject["ID"];
            JSONObject horseInfoJSON = new JSONObject();

            Action<string> getItemInfoCallback = (itemInfo) => { 
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                horseInfoJSON = tempArray[0] as JSONObject;
            };

            StartCoroutine(Main.instance.web.GetHorse(itemID, getItemInfoCallback));

            yield return new WaitUntil(() => isDone);

            GameObject horseGO = Instantiate(Resources.Load("Prefabs/Horse") as GameObject);
            Item item = horseGO.AddComponent<Item>();
            item.ID = id;
            item.ItemID = id;
            horseGO.transform.SetParent(transform);
            horseGO.transform.localScale = Vector3.one;
            horseGO.transform.localPosition = Vector3.zero;

            string color = horseInfoJSON["color"];

            //fill info
            horseGO.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = horseInfoJSON["name"];
            horseGO.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = horseInfoJSON["price"];

            int imgVer = horseInfoJSON["imgVer"].AsInt;

            byte[] bytes = ImageManager.Instance.LoadImage(itemID, imgVer);

            if (bytes.Length == 0)
            {
                Action<byte[]> getItemSpriteCallback = (downloadedBytes) =>
                {
                    Sprite sprite = ImageManager.Instance.BytesToSprite(downloadedBytes);
                    horseGO.transform.Find("Image").GetComponent<Image>().sprite = sprite;
                    ImageManager.Instance.SaveImage(itemID, downloadedBytes, imgVer);
                    ImageManager.Instance.SaveVersionJson();
                };
                horseGO.transform.Find("HorseImage").GetComponent<Image>().sprite = Main.instance.horseHides.GetHorseSprite(color);
                horseGO.transform.Find("MarkingImage").GetComponent<Image>().sprite = Main.instance.horseHides.GetHorseSprite(color);
            } else
            {
                Sprite sprite = ImageManager.Instance.BytesToSprite(bytes);
                horseGO.transform.Find("Image").GetComponent<Image>().sprite = sprite;
            }


                // Set Sell button
                horseGO.transform.Find("Sell Button").GetComponent<Button>().onClick.AddListener(() =>
                {
                    string userItemId = id;
                    string iID = itemID;
                    string userID = Main.instance.userInfo.userID;

                    StartCoroutine(Main.instance.web.SellHorse(userItemId, iID, userID));
                });
        }


        yield return null;
    }
}
