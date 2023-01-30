using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Sc_CameraRaycast : MonoBehaviour
{
    [SerializeField]
    private Clickable clickableHovered;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500, 1 << 3))
        {
            if (clickableHovered != null && clickableHovered.gameObject != hit.transform.gameObject)
            {
                clickableHovered.SetHover(false);
            }
            if(hit.transform.TryGetComponent(out Clickable clickable))
            {
                clickable.SetHover(true);
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                ProcessLeftClick();
            }
        }
        else
        {
            if (clickableHovered != null)
            {
                clickableHovered.SetHover(false);
            }
            clickableHovered = null;
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            ProcessRightClick();
        }
    }

    private void ProcessLeftClick()
    {
        if (clickableHovered == null) return;
        clickableHovered.OnClick();
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
