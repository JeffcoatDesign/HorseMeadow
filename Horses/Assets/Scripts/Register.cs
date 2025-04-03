using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    public TMP_InputField UsernameInput, PasswordInput, ConfirmPasswordInput;
    public Button submitButton, backButton;
    public GameObject loginScreen;
    private void Start()
    {
        submitButton.onClick.AddListener(() =>
        {
            if (PasswordInput.text == ConfirmPasswordInput.text)
            {
                StartCoroutine(Main.instance.web.RegisterUser(UsernameInput.text, PasswordInput.text));
            }
        });
        backButton.onClick.AddListener(() =>
        {
            loginScreen.SetActive(true);
            gameObject.SetActive(false);
        });
    }
}
