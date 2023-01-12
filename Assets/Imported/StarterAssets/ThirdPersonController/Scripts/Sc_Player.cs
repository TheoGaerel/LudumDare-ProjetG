using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_Player : MonoBehaviour
{
    private static Sc_Player sc_instance;

    public static Sc_Player Instance
    {
        get
        {
            if (sc_instance == null)
            {
                sc_instance = FindObjectOfType<Sc_Player>();
            }
            return sc_instance;
        }
    }
    [SerializeField]
    private float f_recallDelay = 0.0f;
    [SerializeField]
    private float F_MAXRECALLDELAY = 0.25f;
    [SerializeField]
    public float F_RECALLRANGE = 2f;
    [SerializeField]
    public float F_INTERACT_RANGE = 2f;

    public List<Sc_RabbitData> list_rabbits = new List<Sc_RabbitData>();
    public List<Sc_RabbitData> list_rabbitsSelected = new List<Sc_RabbitData>();
    [SerializeField]
    private Transform trsf_recallSprite;
    [SerializeField]
    private GameObject go_ArrowMove;

    private void Start()
    {
        trsf_recallSprite.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (f_recallDelay > 0) f_recallDelay -= Time.deltaTime;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {

            OnClickEKey();
        }
        if (Keyboard.current.fKey.wasPressedThisFrame && f_recallDelay <= 0.0f)
        {
            f_recallDelay = F_MAXRECALLDELAY;
            StartCoroutine(RoutineExpandRecallSprite());
            RecallRabits();
        }
    }

    public void RecallRabits()
    {
        Collider[] colls = Physics.OverlapSphere(this.transform.position, F_RECALLRANGE);
        foreach (Collider col in colls)
        {
            if (col.CompareTag("Rabbit"))
            {
                Sc_RabbitData rabbit = col.GetComponent<Sc_RabbitData>();
                if (rabbit.state != Sc_RabbitData.RabbitState.Lost && rabbit.state != Sc_RabbitData.RabbitState.Exausted)
                {
                    col.GetComponent<Sc_RabbitData>().OnRecall();
                }
            }
        }
    }


    private void OnClickEKey()
    {
        Collider[] colls = Physics.OverlapSphere(this.transform.position, F_INTERACT_RANGE);
        foreach (Collider col in colls)
        {
            if (col.CompareTag("Rabbit"))
            {
                Sc_RabbitData rabbit = col.GetComponent<Sc_RabbitData>();
                if (rabbit.state == Sc_RabbitData.RabbitState.Lost)
                {
                    rabbit.OnRecall();
                    return;
                }
            }
            if (col.CompareTag("KingInteractable"))
            {
                if(col.TryGetComponent<Sc_Lever>(out Sc_Lever lever))
                {
                    lever.OnInteract();
                }
            }
        }
    }

    public void OnAddRabbit(Sc_RabbitData rabbit)
    {
        list_rabbits.Add(rabbit);
        Sc_UIManager.Instance.OnAddRabbit(rabbit);
    }

    public void MoveOrder(Vector3 position)
    {
        if (list_rabbitsSelected.Count == 0) return;

        List<Sc_RabbitData> listCopy = new List<Sc_RabbitData>(list_rabbitsSelected);
        foreach (Sc_RabbitData rabbit in listCopy)
        {
            rabbit.OrderToMove(position);
        }

        go_ArrowMove.gameObject.SetActive(true);
        go_ArrowMove.transform.position = new Vector3(position.x, go_ArrowMove.transform.position.y, position.z);
        StartCoroutine(RoutineHideArrow());
    }


    private IEnumerator RoutineExpandRecallSprite()
    {
        float t = 0f;
        while (trsf_recallSprite.localScale.x < 1.5f)
        {
            yield return null;
            t += Time.deltaTime * 1;
            trsf_recallSprite.localScale = Vector3.Lerp(trsf_recallSprite.localScale, Vector3.one * 1.5f, t);
        }
        yield return new WaitForSecondsRealtime(0.1f);
        trsf_recallSprite.localScale = Vector3.zero;
    }

    private IEnumerator RoutineHideArrow()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        go_ArrowMove.gameObject.SetActive(false);
    }
}
