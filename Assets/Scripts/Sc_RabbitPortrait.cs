using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sc_RabbitPortrait : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Sc_RabbitData rabbitData;
    [SerializeField]
    private Image imgBackground;
    [SerializeField]
    private Image imgNormal;

    [SerializeField]
    private Image imgExaust;
    [SerializeField]
    private Image imgWork;
    public void Init(Sc_RabbitData rabbit)
    {
        this.rabbitData = rabbit;
        this.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (rabbitData == null)
        {
            this.gameObject.SetActive(false);
            return;
        }
        imgBackground.gameObject.SetActive(rabbitData.b_hover || rabbitData.b_selected);
        if (rabbitData.b_selected) imgBackground.color = OutlineEffect.Instance.lineColor0;
        if (rabbitData.b_hover) imgBackground.color = OutlineEffect.Instance.lineColor1;

        if (rabbitData.state == Sc_RabbitData.RabbitState.Exausted) imgExaust.gameObject.SetActive(true);
        else imgExaust.gameObject.SetActive(false);

        if (rabbitData.state == Sc_RabbitData.RabbitState.WaitingToInteract) imgWork.gameObject.SetActive(true);
        else imgWork.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rabbitData.SetHover(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rabbitData.SetHover(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      rabbitData.SetSelected(!rabbitData.b_selected);
    }
}
