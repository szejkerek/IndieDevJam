using GordonEssentials.Types;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueContainer
{
    [SerializeField] private List<DialogueLineSO> lines = new List<DialogueLineSO>();

    int takeCount = 0;
    public DialogueContainer()
    {
        if(lines != null && lines.Count > 0)
        {
            lines.Shuffle();
        }
    }

    public DialogueLineSO TakeElement()
    {
        if (lines.Count <= 0)
        {
            Debug.LogWarning($"There are no dialogue lines in list");
            return null;
        }

        takeCount++;
        if(takeCount % lines.Count == 0)
        {
            lines.Shuffle();
        }

        DialogueLineSO firstElement = lines[0];
        lines.RemoveAt(0);
        lines.Add(firstElement);
        return firstElement;
    }
}
