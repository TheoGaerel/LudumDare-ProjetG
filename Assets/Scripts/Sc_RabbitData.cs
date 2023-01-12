using cakeslice;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected Outline outline;
    public bool b_selected { get; protected set; } = false;
    public bool b_hover { get; protected set; } = false;

    protected virtual void Start()
    {
        outline = GetComponentInChildren<Outline>(true);
        outline.eraseRenderer = true;
    }

    public virtual void SetHover(bool hover)
    {
        b_hover = hover;
        if (b_selected) return;
        outline.color = 1;
        outline.eraseRenderer = !b_hover;
    }

    public abstract void OnClick();
}


public class Sc_RabbitData : Interactable
{
    public enum RabbitState
    {
        Lost,
        Following,
        Waiting,
        WaitingToInteract,
        MovingTo,
        Exausted
    }


    Sc_RabbitController controller;

    public RabbitState state = RabbitState.Lost;//{ get; private set; }
    [SerializeField]
    private Sc_WorldUI sc_rabbitWorldUI = default;
    public Interactable targetInteractable { get; private set; }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        controller = GetComponent<Sc_RabbitController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == RabbitState.Lost)
        {
            WaitForKingNear();
            outline.eraseRenderer = true;
            return;
        }
        if (state == RabbitState.Exausted)
        {
            outline.eraseRenderer = true;
            return;
        }
        if (state == RabbitState.Following)
        {
            controller.FollowPlayer();
        }
    }


    public void OnRecall()
    {
        if (state == RabbitState.Lost)
        {
            sc_rabbitWorldUI.OnJoinPlayer();
            SetState(RabbitState.Following);
            Sc_Player.Instance.OnAddRabbit(this);
        }
        else if (state == RabbitState.Waiting || state == RabbitState.MovingTo || state == RabbitState.WaitingToInteract)
        {
            SetState(RabbitState.Following);
            targetInteractable = null;
        }
    }

    private void WaitForKingNear()
    {
        //Debug.Log(Vector3.Distance(this.transform.position, Sc_Player.Instance.transform.position));
        if (Vector3.Distance(this.transform.position, Sc_Player.Instance.transform.position) < Sc_Player.Instance.F_INTERACT_RANGE)
        {
            sc_rabbitWorldUI.ShowEKey();
        }
        else sc_rabbitWorldUI.HideEKey();
    }

    /// <summary>
    /// Move to position and unselect
    /// </summary>
    /// <param name="position"></param>
    public void OrderToMove(Vector3 position)
    {
        targetInteractable = null;
        SetSelected(false);
        Sc_Player.Instance.list_rabbitsSelected.Remove(this);
        SetState(RabbitState.MovingTo);
        controller.MoveTo(position);
    }
    /// <summary>
    /// Move to position and unselect
    /// </summary>
    /// <param name="position"></param>
    public void OrderToInteract(Interactable interactable)
    {
        targetInteractable = interactable;
        SetSelected(false);
        Sc_Player.Instance.list_rabbitsSelected.Remove(this);
        SetState(RabbitState.MovingTo);
        controller.MoveTo(interactable.transform.position);
    }

    public override void OnClick()
    {
        if (state == RabbitState.Exausted || state == RabbitState.Lost) return;
        SetSelected(!b_selected);
    }

    public override void SetHover(bool hover)
    {
        if (state == RabbitState.Exausted || state == RabbitState.Lost) return;
        base.SetHover(hover);
    }

    public void SetSelected(bool selected)
    {
        if (state == RabbitState.Lost) return;
        b_selected = selected;
        if (b_selected || b_hover) outline.eraseRenderer = false;
        else outline.eraseRenderer = true;

        if (b_selected) outline.color = 0;
        else if (b_hover) outline.color = 1;

        if (b_selected) Sc_Player.Instance.list_rabbitsSelected.Add(this);
        else Sc_Player.Instance.list_rabbitsSelected.Remove(this);
    }

    public void SetState(RabbitState state)
    {
        this.state = state;
    }
}
