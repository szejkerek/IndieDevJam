using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Power : ScriptableObject
{
    [field: SerializeField] public DialogueLineSO StartingDialogueLine { private set; get; }
    abstract protected void UsePower();
}
