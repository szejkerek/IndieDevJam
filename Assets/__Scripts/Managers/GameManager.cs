using GordonEssentials;
using GordonEssentials.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>   
{
    [SerializeField] Transform startpoint;
    [SerializeField] List<InteractibleScrew> endgameScrews;
    int solved = 0;

    public List<Power> possiblePowers;
    bool isReloading = false;

    public bool canRestart = false;
    public bool introDone = false;
    private int currentPowerIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        possiblePowers.Shuffle();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.R) && canRestart)
        {
            RestartGame();
        }
    }

    private void SetNewPower()
    {
        if (currentPowerIndex >= possiblePowers.Count)
            return;
        Player.Instance.TheTool.SetNewPower(possiblePowers[currentPowerIndex]);
        currentPowerIndex++;
    }


    IEnumerator ReloadCooldown()
    {
        isReloading = true;
        yield return new WaitForSeconds(1);
        isReloading = false;
    }

    public void OnIntroCompleted()
    {
        Debug.Log("End of intro");
        InvokeRepeating(nameof(PlayDialogue), 15, 20);
        SetNewPower();
        canRestart = true;
        introDone = true;
    }

    void PlayDialogue()
    {
        DialogueManager.Instance.PlayDefaultLine();
    }

    public void RestartGame()
    {
        if (isReloading) return;
        StartCoroutine(ReloadCooldown());
        
        FadeScreen.Instance.FadeAction(() =>
        {
            DialogueManager.Instance.ClearAndStop();
            Player.Instance.transform.position = startpoint.transform.position;
        });
    }

    bool GameEnded()
    {
        return endgameScrews.Count == solved;
    }

    public void OnUnscrewEndgame()
    {       
        solved++;
        if (GameEnded())
        {
            Debug.Log("Game ended");
            return;
        }

        Debug.Log("Unscrew");
        DialogueManager.Instance.PlayMadLine();
        SetNewPower();
    }

}
