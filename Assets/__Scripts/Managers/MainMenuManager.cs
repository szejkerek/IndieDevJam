using GordonEssentials;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using GordonEssentials.Types;

public class MainMenuManager : Singleton<MainMenuManager>
{
    [SerializeField] Sound mainMusic;
    [SerializeField] Button StartGameBtn;
    [SerializeField] Button QuitGameBtn;

    [SerializeField] Sound clickSound; // Add your click sound here

    void Start()
    {
        AudioManager.Instance.PlayGlobal(mainMusic, SoundType.Music);
        StartGameBtn.onClick.AddListener(() => OnButtonClick(StartGameBtn, () => StartGame()));
        QuitGameBtn.onClick.AddListener(() => OnButtonClick(QuitGameBtn, () => Application.Quit()));
    }

    private void OnButtonClick(Button obj, Action onClickAction)
    {
        AudioManager.Instance.PlayGlobal(clickSound);

        // Button animation using DOTween
        obj.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f)
            .OnComplete(() =>
            {
                obj.transform.DOScale(Vector3.one, 0.2f);
                // Execute the click action after the animation completes
                onClickAction?.Invoke();
            });
    }
    private void StartGame()
    {
        SceneLoader.Instance.LoadScene(SceneConstants.Gameplay);
    }
}
