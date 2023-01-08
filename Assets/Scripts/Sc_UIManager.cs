using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_UIManager : MonoBehaviour
{
    private static Sc_UIManager sc_instance;

    public static Sc_UIManager Instance
    {
        get
        {
            if (sc_instance == null)
            {
                sc_instance = FindObjectOfType<Sc_UIManager>();
            }
            return sc_instance;
        }
    }

    [SerializeField]
    private List<Sc_RabbitPortrait> list_rabbitImages = new List<Sc_RabbitPortrait>();

    public void Start()
    {
        foreach (Sc_RabbitPortrait rabbit in list_rabbitImages) rabbit.gameObject.SetActive(false);
    }

    public void OnAddRabbit(Sc_RabbitData rabbitData)
    {
        list_rabbitImages[Sc_Player.Instance.list_rabbits.Count - 1].Init(rabbitData);
    }
}
