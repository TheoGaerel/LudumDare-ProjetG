using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_RabbitWorldUI : MonoBehaviour
{
    [SerializeField]
    private GameObject go_canJoinPlayer;
    [SerializeField]
    private GameObject go_joinPlayer;


    private void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }
    public void ShowCanJoinPlayer()
    {
        go_canJoinPlayer.SetActive(true);
    }

    public void HideCanJointPlayer()
    {
        go_canJoinPlayer.SetActive(false);
    }

    public void OnJoinPlayer()
    {
        go_canJoinPlayer.SetActive(false);
        go_joinPlayer.SetActive(true);
        StartCoroutine(RoutineOnJoinPlayer());
    }

    private IEnumerator RoutineOnJoinPlayer()
    {
        yield return new WaitForSecondsRealtime(0.35f);
        go_joinPlayer.SetActive(false);
    }
}
