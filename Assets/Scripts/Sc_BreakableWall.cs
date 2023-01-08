using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_BreakableWall : Interactable
{
    [SerializeField]
    [Range(0,20)]
    private int i_rabbitAmountRequired;

    public override void SetHover(bool hover)
    {
        b_hover = hover;
        if (!b_hover) outline.eraseRenderer = true;

        if (i_rabbitAmountRequired > Sc_Player.Instance.list_rabbitsSelected.Count) return;
        if (b_hover) outline.eraseRenderer = false;
    }

    public override void OnClick()
    {
        throw new System.NotImplementedException();
    }
}
