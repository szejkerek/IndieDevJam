using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Button backButton;

    private void Start()
    {
        backButton.onClick.AddListener(() => MainMenuManager.Instance.ShowMainPanel());
    }
}
