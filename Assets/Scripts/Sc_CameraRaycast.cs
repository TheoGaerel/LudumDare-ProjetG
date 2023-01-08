using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Sc_CameraRaycast : MonoBehaviour
{
    [SerializeField]
    private Interactable interactableHovered;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500, 1 << 3))
        {
            if (interactableHovered != null && interactableHovered.gameObject != hit.transform.gameObject)
            {
                interactableHovered.SetHover(false);
            }
            interactableHovered = hit.transform.GetComponent<Interactable>();
            interactableHovered.SetHover(true);

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                ProcessLeftClick();
            }
        }
        else
        {
            if (interactableHovered != null)
            {
                interactableHovered.SetHover(false);
            }
            interactableHovered = null;
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            ProcessRightClick();
        }
    }

    private void ProcessLeftClick()
    {
        if (interactableHovered == null) return;
        interactableHovered.OnClick();
    }

    private void ProcessRightClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500, 1 << 6))
        {
            NavMesh.SamplePosition(hit.point, out NavMeshHit hitNavMesh, 50, NavMesh.AllAreas);


            Sc_Player.Instance.MoveOrder(hitNavMesh.position);

        }
    }
}
