using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sc_WorldUI : MonoBehaviour
{
    [SerializeField]
    private GameObject go_eKey;
    [SerializeField]
    private GameObject go_joinPlayer;


    private void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }
    public void ShowEKey()
    {
        go_eKey.SetActive(true);
    }

    public void HideEKey()
    {
        go_eKey.SetActive(false);
    }

    public void OnJoinPlayer()
    {
        go_eKey.SetActive(false);
        go_joinPlayer.SetActive(true);
        StartCoroutine(RoutineOnJoinPlayer());
    }

    private IEnumerator RoutineOnJoinPlayer()
    {
        yield return new WaitForSecondsRealtime(0.35f);
        go_joinPlayer.SetActive(false);
    }
}
