using GordonEssentials;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] private DialogueContainer defaultLines;
    [SerializeField] private DialogueContainer madLines;
    private Queue<DialogueLineSO> dialogQueue;

    public bool dialogueIsBlocked = false;
    public bool madLinePlayed = false;

    private void Start()
    {
        base.Awake();
        if (FindObjectsOfType<DialogueManager>().Length > 1) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        dialogQueue = new Queue<DialogueLineSO>();
        dialogueText.text = string.Empty;
    }

    public void Play(DialogueLineSO dialogueToPlay)
    {
        if (dialogueIsBlocked)
        {
            Debug.LogWarning($"Dialogue is still blocked.");
            dialogQueue.Enqueue(dialogueToPlay);
            return;
        }
        StartCoroutine(PlayDialogue(dialogueToPlay));
    }

    public void ClearAndStop()
    {
        dialogQueue.Clear();
        foreach (AudioSource s in AudioManager.Instance.gameObject.GetComponents<AudioSource>())
        {
            s.Stop();
        }
        dialogueText.text = "";
    }

    public void PlayDefaultLine()
    {
        Play(defaultLines.TakeElement());
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

        if (dialogueToPlay.madLine) madLinePlayed = true;

        dialogueIsBlocked = false;

        if (dialogQueue.Count > 0) Play(dialogQueue.Dequeue());
    }

}
