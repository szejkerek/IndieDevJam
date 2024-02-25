using GordonEssentials.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DialogueLine", menuName = "Audio/DialogueLine")]
public class DialogueLineSO : Sound
{
    [field: SerializeField] public bool madLine = false;
    [field: SerializeField] public string Subtitles { private set; get; }
}
