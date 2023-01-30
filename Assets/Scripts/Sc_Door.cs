using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class Interactable : MonoBehaviour
{
    public abstract void OnInteract();
}

public class Sc_Door : Interactable
{
    private BoxCollider boxCollider;
    private NavMeshObstacle navmeshObstacle;
    private Animator animator;
    private bool b_Open = false;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        navmeshObstacle = GetComponent<NavMeshObstacle>();
        animator = GetComponent<Animator>();
    }


    public override void OnInteract()
    {
        if (animator)
        {
            b_Open = !b_Open;
            animator.SetBool("b_Open", b_Open);
            if (boxCollider) boxCollider.enabled = !b_Open;
            if (navmeshObstacle) navmeshObstacle.enabled = !b_Open;
        }
    }
}
