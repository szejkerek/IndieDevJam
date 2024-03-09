using GordonEssentials;
using GordonEssentials.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>   
{
    public Slider durabilityBar;
    [SerializeField] Sound music;
    [SerializeField] Transform startpoint;
    [SerializeField] List<InteractibleScrew> endgameScrews;
    int solved = 0;

    public List<Power> possiblePowers;
    bool isReloading = false;

    public bool canRestart = false;
    public bool introDone = false;
    private int currentPowerIndex = 0;
    float hpDecrement = 0;
    protected override void Awake()
    {
        base.Awake();
        possiblePowers.Shuffle();
        durabilityBar.gameObject.SetActive(false);
        durabilityBar.value = 1;
        hpDecrement = 1 / (float)endgameScrews.Count;
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
        AudioManager.Instance.PlayGlobal(music);
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

    void EndGame()
    {
        SceneLoader.Instance.LoadScene(SceneConstants.MainMenu);
    }

    public void OnUnscrewEndgame()
    {       
        Debug.Log("Unscrew");
        solved++;
        durabilityBar.value -= hpDecrement;

        if (endgameScrews.Count == solved)
        {
            Debug.Log("Game ended");
            SceneLoader.Instance.LoadScene(SceneConstants.MainMenu);
        }
        else
        {
            DialogueManager.Instance.PlayMadLine();
            SetNewPower();
        }
    }

}
