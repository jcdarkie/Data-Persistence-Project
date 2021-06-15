using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public GameObject playerNameContainer;
    public GameObject mainMenuContainer;

    public TMP_Text savedPlayerName;
    public TMP_InputField playerNameInputField;

    public Button continueButton;

    public string playerName;

    private void Start()
    {
        playerNameContainer.SetActive(true);
        mainMenuContainer.SetActive(false);
        continueButton.interactable = false;

        playerNameInputField.onValueChanged.AddListener(delegate { ControlContinueButton(); });
    }

    public void TogglePlayerNameMenu()
    {
        if(playerNameContainer.activeSelf && !mainMenuContainer.activeSelf)
        {
            playerNameContainer.SetActive(false);
        }
        else
        {
            playerNameContainer.SetActive(true);
        }
        
    }

    public void ControlContinueButton()
    {
        playerName = playerNameInputField.text;

        if(!string.IsNullOrEmpty(playerName))
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
    }

    public void ChangeMenu()
    {
        TogglePlayerNameMenu();

        DataManager.instance.playerName = playerName;
        
        mainMenuContainer.SetActive(true);

        savedPlayerName.text = "Hello " + playerName + "!";
    }

    public void StartGame()
    {
        DataManager.instance.SavePlayerName();
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
