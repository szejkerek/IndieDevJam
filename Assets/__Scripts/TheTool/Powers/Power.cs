using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Power : ScriptableObject
{
    [field: SerializeField] public DialogueLineSO StartingDialogueLine { private set; get; }
    [field: SerializeField] public DialogueContainer UsageDialogues { private set; get; }
    [field: SerializeField] public Color PowerColor { private set; get; }
    abstract public void UsePower();
}
