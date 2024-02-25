using GordonEssentials;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>   
{
    [SerializeField] List<InteractibleScrew> interactibleScrewList;
    [SerializeField] Slider durability;


    protected override void Awake()
    {
        base.Awake();
        durability.gameObject.SetActive(false);
    }

    public void OnIntroCompleted()
    {
        durability.gameObject.SetActive(true);
        Debug.Log("End of intro");
    }
}
