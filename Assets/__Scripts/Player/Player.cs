using GordonEssentials;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public Camera PlayerCamera => playerCamera;
    [SerializeField] Camera playerCamera;
    public TheTool TheTool => theTool;
    public GameObject paperworkPivot;
    [SerializeField] TheTool theTool;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public Vector3 GetLookRotation()
    {
        if (playerCamera == null)
        {
            Debug.LogError("Player camera is not assigned!");
            return Vector3.zero;
        }

        Vector3 lookRotation = playerCamera.transform.rotation.eulerAngles;
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
