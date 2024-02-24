using GordonEssentials;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] Camera playerCamera;
    public TheTool TheTool => theTool;
    [SerializeField] TheTool theTool;

    public Vector3 GetLookRotation()
    {
        if (playerCamera == null)
        {
            Debug.LogError("Player camera is not assigned!");
            return Vector3.zero;
        }

        Vector3 lookRotation = playerCamera.transform.forward;
        return lookRotation;
    }

    private void Update()
    {
        Debug.DrawLine(playerCamera.transform.position, GetLookRotation());
    }

    public void SpawnToolInHand() 
    {
        TheTool.gameObject.SetActive(true);
    }

}
