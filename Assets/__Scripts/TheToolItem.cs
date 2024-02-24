using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheToolItem : MonoBehaviour, IPickUpble
{
    public void OnPickUp()
    {
        gameObject.SetActive(false);
        Player.Instance.SpawnToolInHand();
    }
}
