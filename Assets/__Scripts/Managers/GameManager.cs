using GordonEssentials;
using GordonEssentials.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : Singleton<GameManager>   
{
    [SerializeField] Transform startpoint;
    [SerializeField] List<InteractibleScrew> endgameScrews;

    public List<Power> possiblePowers;
    bool canRestart = false;
    bool isReloading = false;

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
            string currentSceneName = SceneManager.GetActiveScene().name;
            DialogueManager.Instance.ClearAndStop();
            SceneManager.LoadScene(currentSceneName);
        });
    }

    public void OnUnscrewEndgame()
    {
        Debug.Log("Unscrew");
        DialogueManager.Instance.PlayMadLine();
        SetNewPower();
    }

}
