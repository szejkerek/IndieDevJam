using GordonEssentials;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>   
{
    [SerializeField] List<InteractibleScrew> interactibleScrewList;
    [SerializeField] Slider durability;
    [SerializeField] Transform startpoint;

    List<GameObject> trash = new List<GameObject>();
    bool canRestart = false;
    protected override void Awake()
    {
        base.Awake();
        durability.gameObject.SetActive(false);
        OnIntroCompleted();
        foreach(var screw in interactibleScrewList)
        {
            screw.onUnscrew.AddListener(OnUnscrewEndgame);
        }
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.R) && canRestart)
        {
            RestartGame();
        }
    }

    public void OnIntroCompleted()
    {
        Debug.Log("End of intro");
        durability.gameObject.SetActive(true);
        //Player.Instance.TheTool.PlayStartingDialogue(); 
        canRestart = true;
    }

    public void AddObjectToTrash(GameObject obj)
    {
        trash.Add(obj);
    }
    private void ClearTrash()
    {
        for (int i = 0; i < trash.Count; i++)
        {
            Destroy(trash[i]);
        }
        trash.Clear();
    }

    public void RestartGame()
    {
        Transform player = FindObjectOfType<Player>().transform;

        FadeScreen.Instance.FadeAction(() =>
        {
            ClearTrash();
            player.transform.position = startpoint.position;
        });
        
       // Invoke(nameof(DialogueManager.Instance.PlayDefaultLine), 2);
    }

    public void OnUnscrewEndgame()
    {
        Debug.Log("Unscrew");
        DialogueManager.Instance.PlayMadLine();
    }


}
