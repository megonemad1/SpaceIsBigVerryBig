using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAnimationExit : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    int stateIndex = 0;

    public int animationIndex = 0;
    // Use this for initialization
    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public void RunAnimation()
    {
        stateIndex = animationIndex;
        animator.SetInteger("Index", stateIndex);
    }
    // Update is called once per frame
    void DestroyObject()
    {
        Destroy(gameObject);

    }
    void DestroyObjectChildren()
    {
        foreach (var g in transform.GetComponentsInChildren<Transform>())
            Destroy(g.gameObject);

    }
}
