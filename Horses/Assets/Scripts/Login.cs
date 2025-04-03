using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public TMP_InputField UsernameInput, PasswordInput;
    public Button loginButton, registerButton;
    public GameObject registerScreen;
    private void Start()
    {
        loginButton.onClick.AddListener(() =>
        {
            StartCoroutine(Main.instance.web.Login(UsernameInput.text, PasswordInput.text));
        });
        registerButton.onClick.AddListener(() =>
        {
            registerScreen.SetActive(true);
            gameObject.SetActive(false);
        });
    }
}
