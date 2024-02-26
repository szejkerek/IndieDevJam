using GordonEssentials;
using GordonEssentials.Types;
using UnityEngine;
using UnityEngine.Events;

public class InteractibleScrew : MonoBehaviour, IInteractable
{
    public bool BlockInteractions = false;
    public Sound UnscrewSound;
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
        if (animator == null) return;
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && startedAnimation)
        {
            OnAnimationEnd();
            startedAnimation = false;
        }
    }

    private void OnAnimationEnd()
    {
        if (GetComponent<Animator>() != null) Destroy(GetComponent<Animator>());
        rb.constraints = RigidbodyConstraints.None;
    }

    public void OnInteract()
    {
        if (BlockInteractions || unscrewedOnce)
            return;

        AudioManager.Instance.PlayAtPosition(transform.position, UnscrewSound);
        startedAnimation = true;
        animator.SetBool("Animate", true);

        unscrewedOnce = true;

        onUnscrew?.Invoke();
    }

}
