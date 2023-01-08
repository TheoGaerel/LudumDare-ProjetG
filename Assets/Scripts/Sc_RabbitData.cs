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
        MovingTo,
        Exausted
    }


    Sc_RabbitController controller;

    public RabbitState state = RabbitState.Lost;//{ get; private set; }
    [SerializeField]
    private Sc_RabbitWorldUI sc_rabbitWorldUI = default;


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
        else if (state == RabbitState.Waiting || state == RabbitState.MovingTo)
        {
            SetState(RabbitState.Following);
        }
    }

    private void WaitForKingNear()
    {
        //Debug.Log(Vector3.Distance(this.transform.position, Sc_Player.Instance.transform.position));
        if (Vector3.Distance(this.transform.position, Sc_Player.Instance.transform.position) < Sc_Player.Instance.F_JOINRANGE)
        {
            sc_rabbitWorldUI.ShowCanJoinPlayer();
        }
        else sc_rabbitWorldUI.HideCanJointPlayer();
    }

    public void OrderToMove(Vector3 position)
    {
        SetSelected(false);
        SetState(RabbitState.MovingTo);
        controller.MoveTo(position);
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
    }

    public void SetState(RabbitState state)
    {
        this.state = state;
    }
}
