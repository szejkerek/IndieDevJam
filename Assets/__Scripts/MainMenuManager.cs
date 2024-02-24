using GordonEssentials;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : Singleton<MainMenuManager>
{
    [SerializeField] Button StartGameBtn;
    [SerializeField] Button OptionsBtn;
    [SerializeField] Button QuitGameBtn;
    [SerializeField] GameObject MainPanel;
    [SerializeField] GameObject OptionsPanel;

    protected override void Awake()
    {
        base.Awake();
        StartGameBtn.onClick.AddListener(() => StartGame());
        OptionsBtn.onClick.AddListener(() => ShowOptionsPanel());
        QuitGameBtn.onClick.AddListener(() => Application.Quit());
        ShowMainPanel();
    }
    public void ShowMainPanel()
    {
        MainPanel.SetActive(true);
        OptionsPanel.SetActive(false);
    }

    private void ShowOptionsPanel()
    {
        MainPanel.SetActive(false);
        OptionsPanel.SetActive(true);
    }

    private void StartGame()
    {
        SceneLoader.Instance.LoadScene(SceneConstants.Gameplay);
    }
}
