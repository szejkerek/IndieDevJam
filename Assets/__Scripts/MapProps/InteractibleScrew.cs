using UnityEngine;
using UnityEngine.Events;

public class InteractibleScrew : MonoBehaviour, IInteractable
{
    public bool BlockInteractions = false;
    public UnityEvent onUnscrew;


    Animator animator;
    Rigidbody rb;
    bool startedAnimation = false;
    bool unscrewedOnce = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && startedAnimation)
        {
            OnAnimationEnd();
            startedAnimation = false;
        }
    }

    private void OnAnimationEnd()
    {
        rb.constraints = RigidbodyConstraints.None;
    }

    public void OnInteract()
    {
        if (BlockInteractions || unscrewedOnce)
            return;

        startedAnimation = true;
        animator.SetBool("Animate", true);
        onUnscrew?.Invoke();
        unscrewedOnce = true;

        GameManager.Instance.AddObjectToTrash(gameObject);
    }
}
