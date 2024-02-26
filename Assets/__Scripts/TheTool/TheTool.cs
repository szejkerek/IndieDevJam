using GordonEssentials;
using GordonEssentials.Types;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TheTool : MonoBehaviour
{
    public static Action OnSpawnedInHands;

    [SerializeField] private float cooldown;
    private float lastUseTime;

    Power currentPower; 

     void Awake()
     {
        OnSpawnedInHands?.Invoke();        
     }

    public void PlayStartingDialogue()
    {
        DialogueManager.Instance.Play(currentPower.StartingDialogueLine);
    }

    private void Update()
    {
        if (IsCooldownExpired())
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                PerformDefaultToolInteraction();
                lastUseTime = Time.time;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                UseCurrentPower();
                lastUseTime = Time.time;
            }
        }
    }

    private bool IsCooldownExpired()
    {
        return Time.time - lastUseTime >= cooldown;
    }

    private void PerformDefaultToolInteraction()
    {
        if (Player.Instance.PlayerVision.PlayerRaycast(out Collider hit, Mathf.Infinity))
        {
            if (hit.gameObject.GetComponent<IInteractable>() != null)
            {
                hit.gameObject.GetComponent<IInteractable>().OnInteract();
            }
        }


        Debug.Log("Default Tool Interaction");
    }

    public void SetNewPower(Power power)
    {
        currentPower = power;
        PlayStartingDialogue();
    }

    private void UseCurrentPower()
    {
        if (currentPower != null)
        {
            currentPower.UsePower();
        }
        else
        {
            Debug.LogWarning("No power assigned to the tool.");
        }
    }
}
