using GordonEssentials;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public Camera PlayerCamera => playerCamera;
    Camera playerCamera;
    public TheTool TheTool => theTool;
    TheTool theTool;    
    public PlayerVision PlayerVision => playerVision;
    PlayerVision playerVision;

    public GameObject paperworkPivot;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        theTool = GetComponentInChildren<TheTool>();
        theTool.gameObject.SetActive(false);

        playerCamera = GetComponentInChildren<Camera>();
        playerVision = GetComponent<PlayerVision>();
    }

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

    public void SpawnToolInHand() 
    {
        TheTool.gameObject.SetActive(true);
    }

}
