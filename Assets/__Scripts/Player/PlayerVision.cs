using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{
    [SerializeField] TMP_Text HelpObject;
    [SerializeField] float raycastDistance;
    [SerializeField] float rayCooldown;
    float lastRayTime;
    Player player;
    bool helpVisible = false;

    private void Start()
    {
        player = Player.Instance;
        HelpObject.gameObject.SetActive(false);
    }


    void Update()
    {
        if (Time.time - lastRayTime < rayCooldown)
            return;


        if (PlayerRaycast(out Collider hit, raycastDistance))
        {
            lastRayTime = Time.time;

            if (hit.TryGetComponent(out IInteractable pickUpble))
            {
                ShowHelp("[E] Interact");
                if (Input.GetKey(KeyCode.E))
                {
                    pickUpble.OnInteract();         
                }
            }

            if (hit.TryGetComponent(out IUseable useable))
            {
                ShowHelp("[LMB] Use");
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    useable.OnUse();
                }
            }
        }
        else
        {
            HideHelp();
        }
    }

    public bool PlayerRaycast(out Collider collider, float distance)
    {
        collider = null;
        lastRayTime = Time.time;
        if (Physics.Raycast(player.PlayerCamera.transform.position, player.GetLookRotation(), out RaycastHit hit, distance))
        {
            collider = hit.collider;
            return true;
        }

        return false;
    }

    void ShowHelp(string text)
    {
        if (helpVisible)
            return;
        HelpObject.text = text;
        HelpObject.gameObject.SetActive(true);
        helpVisible = true;
    }

    void HideHelp()
    {
        if (!helpVisible)
            return;

        HelpObject.gameObject.SetActive(false);
        helpVisible = false;
    }
}
