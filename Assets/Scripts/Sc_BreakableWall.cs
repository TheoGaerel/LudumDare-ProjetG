using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_BreakableWall : Clickable
{
    [SerializeField]
    [Range(0, 20)]
    private int i_rabbitAmountRequired;

    private List<Sc_RabbitData> list_rabbits = new List<Sc_RabbitData>();
    private bool b_usable = true;

    private void Update()
    {
        if (list_rabbits.Count > 0 && b_usable)
        {
            int count = 0;
            foreach (Sc_RabbitData rabbitData in list_rabbits)
            {
                if (rabbitData.state == Sc_RabbitData.RabbitState.WaitingToInteract) count++;
            }

            if (count >= i_rabbitAmountRequired)
            {
                b_usable = false;
                BreakWall();
            }
        }
    }

    public override void SetHover(bool hover)
    {
        b_hover = hover;
        if (!b_hover) outline.eraseRenderer = true;

        if (Sc_Player.Instance.list_rabbitsSelected.Count > 0)
        {
            if (b_hover) outline.eraseRenderer = false;
        }
    }

    public override void OnClick()
    {
        for (int i = 0; i < Sc_Player.Instance.list_rabbitsSelected.Count; i++)
        {
            if (list_rabbits.Count < i_rabbitAmountRequired)
            {
                list_rabbits.Add(Sc_Player.Instance.list_rabbitsSelected[i]);
            }
            else break;
        }

        foreach (Sc_RabbitData rabbitData in list_rabbits) rabbitData.OrderToInteract(this);
    }

    private void BreakWall()
    {
        Debug.Log("Break Wall !");
        b_usable = false;

        foreach (Sc_RabbitData rabbitData in list_rabbits) rabbitData.SetState(Sc_RabbitData.RabbitState.Exausted);
        StartCoroutine(RoutineBreakWall());
    }

    private IEnumerator RoutineBreakWall()
    {
        yield return new WaitForSeconds(2f);
        if (GetComponentInChildren<Canvas>()) GetComponentInChildren<Canvas>().gameObject.SetActive(false);
        if (GetComponentInChildren<Canvas>()) GetComponentInChildren<Canvas>().gameObject.SetActive(false);
        if (GetComponentInChildren<Collider>()) GetComponentInChildren<Collider>().gameObject.SetActive(false);
    }
}
