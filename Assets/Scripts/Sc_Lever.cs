using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Lever : MonoBehaviour
{
    [SerializeField]
    private Sc_WorldUI sc_WorldUI = default;
    bool b_canUse = true;
    [SerializeField]
    private Transform trsf_rotationPoint;
    [SerializeField]
    private GameObject linkedObject;

    private void Update()
    {
        if (Vector3.Distance(this.transform.position, Sc_Player.Instance.transform.position) < Sc_Player.Instance.F_INTERACT_RANGE && b_canUse)
        {
            sc_WorldUI.ShowEKey();
        }
        else sc_WorldUI.HideEKey();
    }

    public void OnInteract()
    {
        if (!b_canUse) return;
        b_canUse = false;
        StartCoroutine(RoutineRotateLever());
        if (linkedObject)
        {

        }
    }

    private IEnumerator RoutineRotateLever()
    {
        float angleZ = trsf_rotationPoint.eulerAngles.z;
        while (trsf_rotationPoint.eulerAngles.z > -40)
        {
            angleZ -= Time.deltaTime * 100f;
            trsf_rotationPoint.eulerAngles = new Vector3(trsf_rotationPoint.eulerAngles.x, trsf_rotationPoint.eulerAngles.y, angleZ);
            yield return null;
        }
    }
}
