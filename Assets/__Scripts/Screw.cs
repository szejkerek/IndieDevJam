using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Screw : MonoBehaviour, IInteractable
{
    [SerializeField] UnityEvent onInteract;
    public void OnInteract()
    {
        onInteract?.Invoke();
    }
}