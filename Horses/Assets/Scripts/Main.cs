using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Web))]
[RequireComponent(typeof(HorseHides))]
[RequireComponent(typeof(UserInfo))]
public class Main : MonoBehaviour
{
    public static Main instance;
    public Web web;
    public HorseHides horseHides;
    public HorsesManager horsesManager;
    public UserInfo userInfo;
    public Login login;
    public GameObject userProfile;

    private void Awake()
    {
        instance = this;
        web = GetComponent<Web>();
        userInfo = GetComponent<UserInfo>();
    }

    private void Reset()
    {
        if (horseHides == null) horseHides = gameObject.AddComponent<HorseHides>();
        if (web == null) web = gameObject.AddComponent<Web>();
        if (userInfo == null) userInfo = gameObject.AddComponent<UserInfo>();
    }
}
