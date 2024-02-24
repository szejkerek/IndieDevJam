using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{
    [SerializeField] GameObject HelpObject;
    [SerializeField] float raycastDistance;
    Player player;
    bool helpVisible = false;
    private void Start()
    {
        player = Player.Instance;
        HelpObject.SetActive(false);
    }


    void Update()
    {
        Vector3 dir = player.GetLookRotation();

        RaycastHit hit;
        if (Physics.Raycast(player.PlayerCamera.transform.position, dir, out hit, raycastDistance))
        {
            IPickUpble pickUpble = hit.collider.GetComponent<IPickUpble>();

            if (pickUpble != null)
            {
                ShowHelp();
                if(Input.GetKeyDown(KeyCode.E))
                {
                    pickUpble.OnPickUp();
                }
            }
        }
        else
        {
            HideHelp();
        }
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
