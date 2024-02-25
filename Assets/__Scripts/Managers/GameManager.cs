using GordonEssentials;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>   
{

    protected override void Awake()
    {
    
    }

    public void OnIntroCompleted()
    {
        Debug.Log("End of intro");
    }
}
