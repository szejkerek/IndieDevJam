using GordonEssentials;
using GordonEssentials.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Power : ScriptableObject
{
    [field: SerializeField] public DialogueLineSO StartingDialogueLine { private set; get; }
    [field: SerializeField] public List<Sound> UseSound { private set; get; }
    [field: SerializeField] public Color PowerColor { private set; get; }
    abstract public void UsePower();

    protected void PlayUseSound()
    {
        AudioManager.Instance.PlayAtPosition(Player.Instance.transform.position,UseSound.SelectRandomElement());
    }
}
