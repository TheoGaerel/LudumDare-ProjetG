using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_TriggerChangeCamera : MonoBehaviour
{
    public Sc_MainCamera.CameraDirection direction;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Sc_MainCamera.Instance.SetCameraDirection(direction);
    }
}
