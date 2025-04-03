using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    public string userID { get; private set; }
    public string username { get; private set; }
    public string password { get; private set; }
    public string level { get; private set; }
    public string coins { get; private set; }

    public void SetCredentials(string username, string password)
    {
        this.username = username;
        this.password = password;
    }

    public void SetID(string userID) => this.userID = userID;
}
