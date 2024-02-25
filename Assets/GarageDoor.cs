using UnityEngine;
using DG.Tweening;

public class GarageDoor : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private float height = 5f;
    [SerializeField] private float duration = 1.5f;

    // Method to open the garage door using DOTween animation
    public void OpenDoor()
    {      
        door.transform.DOMove(transform.position + Vector3.up * height, duration)
            .SetEase(Ease.Linear)
            .OnComplete(OnDoorOpened);
    }

    private void OnDoorOpened()
    {
        Debug.Log("Garage door is now open!");
    }
}
