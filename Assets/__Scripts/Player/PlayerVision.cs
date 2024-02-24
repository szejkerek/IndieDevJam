using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{
    [SerializeField] GameObject HelpObject;
    [SerializeField] float raycastDistance;
    [SerializeField] float rayCooldown;
    float lastRayTime;
    Player player;
    bool helpVisible = false;

    private void Start()
    {
        player = Player.Instance;
        HelpObject.SetActive(false);
    }


    void Update()
    {
        if (Time.time - lastRayTime < rayCooldown)
            return;


        if (PlayerRaycast(out Collider hit, raycastDistance))
        {
            IInteractable pickUpble = hit.GetComponent<IInteractable>();
            lastRayTime = Time.time;

            if (pickUpble != null)
            {
                ShowHelp();
                if (Input.GetKey(KeyCode.E))
                {
                    pickUpble.OnInteract();         
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




    void ShowHelp()
    {
        if (helpVisible)
            return;

        HelpObject.SetActive(true);
        helpVisible = true;
    }

    void HideHelp()
    {
        if (!helpVisible)
            return;

        HelpObject.SetActive(false);
        helpVisible = false;
    }
}
