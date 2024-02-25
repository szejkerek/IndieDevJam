using GordonEssentials;
using GordonEssentials.Types;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TheTool : MonoBehaviour
{
    public static Action OnSpawnedInHands;
    public static Action OnDeath;

    [SerializeField] private float cooldown;
    private float lastUseTime;

    public List<Power> possiblePowers;
    int maxHealth;
    int currentHealth;
    [SerializeField] private Power currentPower; 

     void Awake()
     {
        maxHealth = possiblePowers.Count;
        currentHealth = maxHealth;
        OnSpawnedInHands?.Invoke();        
     }

    public void InitializePowers()
    {
        maxHealth = possiblePowers.Count;
        currentHealth = maxHealth;
        GetNextSkill();
    }

    public void TakeDamage()
    {
        currentHealth--;
        if(currentHealth <= 0 ) {
            OnDeath?.Invoke();
        }
    }

    public void GetNextSkill()
    {
        if (possiblePowers.Count <= 0)
        {
            Debug.Log("No more powers");
            return;
        }

        currentPower = possiblePowers[0];
        //possiblePowers.RemoveAt(0);
        //GameManager.Instance.possiblePowers.Remove(currentPower);
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

    private void UseCurrentPower()
    {
        if (currentPower != null)
        {
            currentPower.UsePower();
            //DialogueManager.Instance.Play(currentPower.UsageDialogues.TakeElement());
        }
        else
        {
            Debug.LogWarning("No power assigned to the tool.");
        }
    }
}
