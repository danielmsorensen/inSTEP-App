using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour {

    public GameObject loadingPage;

    [Header("Login Page")]
    [SerializeField]
    GameObject loginPage;
    public TMP_InputField loginClassID;
    [SerializeField]
    GameObject loginError;
    [SerializeField]
    TMP_Text loginErrorText;

    [Header("Name Page")]
    [SerializeField]
    GameObject namePage;
    public TMP_Text nameTitle;
    public TMP_InputField nameName;
    [SerializeField]
    public GameObject nameError;
    [SerializeField]
    public TMP_Text nameErrorText;

    [Header("Game Page")]
    [SerializeField]
    GameObject gamePage;
    public TMP_Text gameTitle;
    public TMP_Text gameText;

    void Start() {
        PageLogin();

        LoginHideError();
        NameHideError();
    }

    public void PageLogin() {
        loadingPage.SetActive(false);

        loginPage.SetActive(true);
        namePage.SetActive(false);
        gamePage.SetActive(false);
    }

    public void PageName() {
        loadingPage.SetActive(false);

        loginPage.SetActive(false);
        namePage.SetActive(true);
        gamePage.SetActive(false);
    }

    public void PageGame() {
        loadingPage.SetActive(false);

        loginPage.SetActive(false);
        namePage.SetActive(false);
        gamePage.SetActive(true);
    }

    public void NameShowError(string error) {
        if (!string.IsNullOrEmpty(error)) {
            nameError.SetActive(true);
            nameErrorText.SetText(error);
        }
        else {
            NameHideError();
        }
    }
    public void NameHideError() {
        nameError.SetActive(false);
    }

    public void LoginShowError(string error) {
        if (!string.IsNullOrEmpty(error)) {
            loginError.SetActive(true);
            loginErrorText.SetText(error);
        }
        else {
            LoginHideError();
        }
    }
    public void LoginHideError() {
        loginError.SetActive(false);
    }
}
