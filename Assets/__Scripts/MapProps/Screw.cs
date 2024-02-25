using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Screw : MonoBehaviour, IInteractable
{
    public bool BlockInteractions = false;
    [SerializeField] UnityEvent onInteract;
    public void OnInteract()
    {
        if (BlockInteractions)
            return;

        onInteract?.Invoke();
    }
}
