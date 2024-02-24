using GordonEssentials;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : Singleton<MainMenuManager>
{
    [SerializeField] Button StartGameBtn;
    [SerializeField] Button QuitGameBtn;

    protected override void Awake()
    {
        base.Awake();
        StartGameBtn.onClick.AddListener(() => StartGame());
        QuitGameBtn.onClick.AddListener(() => Application.Quit());
    }

    private void StartGame()
    {
        SceneLoader.Instance.LoadScene(SceneConstants.Gameplay);
    }
}
