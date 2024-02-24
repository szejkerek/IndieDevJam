using UnityEngine;

public class TheTool : MonoBehaviour
{
    [SerializeField] private float cooldown;
    private float lastUseTime;

    private Power currentPower;

    private void Update()
    {
        if (IsCooldownExpired())
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                PerformDefaultToolInteraction();
                lastUseTime = Time.time;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                UseCurrentPower();
                lastUseTime = Time.time;
            }
        }
    }

    private bool IsCooldownExpired()
    {
        return Time.time - lastUseTime >= cooldown;
    }

    private void PerformDefaultToolInteraction()
    {
        Debug.Log("Default Tool Interaction");
    }

    private void UseCurrentPower()
    {
        if (currentPower != null)
        {
            //currentPower.UsePower();
        }
        else
        {
            Debug.LogWarning("No power assigned to the tool.");
        }
    }
}
