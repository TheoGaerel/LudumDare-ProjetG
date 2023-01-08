using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_MainCamera : MonoBehaviour
{
    public enum CameraDirection
    {
        North,
        East,
        South,
        West
    }

    private static Sc_MainCamera sc_instance;

    public static Sc_MainCamera Instance
    {
        get
        {
            if (sc_instance == null)
            {
                sc_instance = FindObjectOfType<Sc_MainCamera>();
            }
            return sc_instance;
        }
    }

    [SerializeField]
    private Transform trsf_cameraFollow;
    public float SmoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField]
    private Transform trsf_North;
    [SerializeField]
    private Transform trsf_East;
    [SerializeField]
    private Transform trsf_South;
    [SerializeField]
    private Transform trsf_West;

    public CameraDirection currentDirection = CameraDirection.North;


    private void Update()
    {
        if (Keyboard.current.upArrowKey.wasPressedThisFrame) SetCameraDirection(CameraDirection.North);
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame) SetCameraDirection(CameraDirection.West);
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame) SetCameraDirection(CameraDirection.East);
        if (Keyboard.current.downArrowKey.wasPressedThisFrame) SetCameraDirection(CameraDirection.South);
    }


    void LateUpdate()
    {
        Vector3 targetPosition = trsf_cameraFollow.position;
        this.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
    }

    public void SetCameraDirection(CameraDirection cameraDirection)
    {
        Transform trsfDirection = null;

        switch (cameraDirection)
        {
            case CameraDirection.North:
                trsfDirection = trsf_North;
                break;
            case CameraDirection.East:
                trsfDirection = trsf_East;
                break;
            case CameraDirection.South:
                trsfDirection = trsf_South;
                break;
            case CameraDirection.West:
                trsfDirection = trsf_West;
                break;
        }

        StartCoroutine(RoutineChangeCameraDirection(trsfDirection));
    }

    private IEnumerator RoutineChangeCameraDirection(Transform trsf)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, trsf.position, t);
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, trsf.rotation, t);
            yield return null;
        }
    }
}
