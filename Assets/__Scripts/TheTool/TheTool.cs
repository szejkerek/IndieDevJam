using GordonEssentials;
using GordonEssentials.Types;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TheTool : Singleton<TheTool>
{
    public static Action OnDeath;

    [SerializeField] private float cooldown;
    private float lastUseTime;

    [SerializeField] private List<Power> possiblePowers;
    int maxHealth;
    int currentHealth;
    private Power currentPower; 

    protected override void Awake()
    {
        base.Awake();
        maxHealth = possiblePowers.Count;
        currentHealth = maxHealth;

        possiblePowers.Shuffle();
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
        possiblePowers.RemoveAt(0);
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
        Debug.Log("Default Tool Interaction");
    }

    private void UseCurrentPower()
    {
        if (currentPower != null)
        {
            currentPower.UsePower();
            DialogueManager.Instance.Play(currentPower.UsageDialogues.TakeElement());
        }
        else
        {
            Debug.LogWarning("No power assigned to the tool.");
        }
    }
}
