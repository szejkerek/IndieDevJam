using GordonEssentials;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] private DialogueContainer defaultLines;
    [SerializeField] private DialogueContainer happyLines;
    [SerializeField] private DialogueContainer madLines;

    bool dialogueIsBlocked = false;
    private void Start()
    {
        dialogueText.text = string.Empty;
    }
    public void Play(DialogueLineSO dialogueToPlay)
    {
        if(dialogueIsBlocked)
        {
            Debug.LogWarning($"Dialogue is still blocked.");
            return;
        }
        StartCoroutine(PlayDialogue(dialogueToPlay));
    }

    public void PlayDefaultLine()
    {
        Play(defaultLines.TakeElement());
    }

    public void PlayHappyLine()
    {
        Play(happyLines.TakeElement());
    }

    public void PlayMadLine()
    {
        Play(madLines.TakeElement());
    }

    IEnumerator PlayDialogue(DialogueLineSO dialogueToPlay)
    {
        dialogueIsBlocked = true;

        if(dialogueToPlay != null )
        {
            dialogueText.text = dialogueToPlay.Subtitles;

            yield return new WaitForSeconds(0.1f);
            AudioManager.Instance.PlayGlobal(dialogueToPlay);

            yield return new WaitForSeconds(dialogueToPlay.Clip.length + 0.1f);
            dialogueText.text = "";

            yield return new WaitForSeconds(0.5f);          
        }

        dialogueIsBlocked = false;
    }

}
