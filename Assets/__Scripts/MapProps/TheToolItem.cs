using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheToolItem : MonoBehaviour, IInteractable
{
    public bool BlockInteractions = false;
    public void OnInteract()
    {
        if (BlockInteractions)
            return;

        gameObject.SetActive(false);
        Player.Instance.SpawnToolInHand();
        IntroManager.Instance.toolPickedUp = true;
    }
}
