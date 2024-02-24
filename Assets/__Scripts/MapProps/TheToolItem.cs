using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheToolItem : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        gameObject.SetActive(false);
        Player.Instance.SpawnToolInHand();
    }
}
