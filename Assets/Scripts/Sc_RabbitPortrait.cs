using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_RabbitPortrait : MonoBehaviour
{
    private Sc_RabbitData rabbitData;
    [SerializeField]
    private Image imgBackground;
    [SerializeField]
    private Image imgNormal;

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
    }
}
