using GordonEssentials;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] Transform cameraHolder;
    public PlayerCamera PlayerCamera => playerCamera;
    PlayerCamera playerCamera;
    public TheTool TheTool => theTool;
    TheTool theTool;    
    public PlayerVision PlayerVision => playerVision;
    PlayerVision playerVision;

    public GameObject paperworkPivot;

    protected override void Awake()
    {
        base.Awake();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        theTool = GetComponentInChildren<TheTool>();
        theTool.gameObject.SetActive(false);

        playerCamera = GetComponentInChildren<PlayerCamera>();
        playerVision = GetComponent<PlayerVision>();

        if (GameManager.Instance != null) GameManager.Instance.SetupAfterRestart();

        cameraHolder.parent = null;
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
