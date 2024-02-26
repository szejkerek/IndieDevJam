using GordonEssentials;
using GordonEssentials.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : Singleton<IntroManager>
{
    [SerializeField] TheToolItem toolItem;
    [SerializeField] InteractibleScrew screw;
    [SerializeField] GameObject lightObject;
    [SerializeField] Sound lightOnSound;
    [Space]
    [SerializeField] DialogueLineSO inSuchPlace;
    [SerializeField] DialogueLineSO speciesDoomed;
    [SerializeField] DialogueLineSO embracePower;
    [SerializeField] DialogueLineSO unscrewHim;


    public bool toolPickedUp = false;
    public bool doorOpen = false;

    public void OnDoorOpen()
    {
        doorOpen = true;
    }

    private void Start()
    {
        lightObject.gameObject.SetActive(false);
        toolItem.BlockInteractions = true;
        screw.BlockInteractions = true;
        StartCoroutine(IntroSequence());
    }

    private IEnumerator IntroSequence()
    {
        yield return new WaitForSeconds(2f);
        lightObject.SetActive(true);
        AudioManager.Instance.PlayAtPosition(lightObject.transform.position, lightOnSound);
        yield return new WaitForSeconds(1f);

        DialogueManager.Instance.Play(inSuchPlace);
        yield return new WaitForSeconds(inSuchPlace.Clip.length + 1f);

        DialogueManager.Instance.Play(speciesDoomed);
        yield return new WaitForSeconds(speciesDoomed.Clip.length + 1f);
        toolItem.BlockInteractions = false;

        while (!toolPickedUp)
            yield return null;

        DialogueManager.Instance.Play(embracePower);
        yield return new WaitForSeconds(embracePower.Clip.length + 1f);
        screw.BlockInteractions = false;

        while (!doorOpen)
            yield return null;


        //===========================TEMPORARY==============================
        toolItem.BlockInteractions = false;
        screw.BlockInteractions = false;

        while (!doorOpen)
            yield return null;
        //==================================================================



        DialogueManager.Instance.Play(unscrewHim);
        yield return new WaitForSeconds(unscrewHim.Clip.length + 1f);
        GameManager.Instance.OnIntroCompleted();

        yield return null;
    }

}
