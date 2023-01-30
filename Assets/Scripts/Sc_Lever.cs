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
    private Interactable linkedObject;

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
        Debug.Log("OnInteract");
        if (!b_canUse) return;
        b_canUse = false;
        StartCoroutine(RoutineRotateLever());
        if (linkedObject != null)
        {
            linkedObject.OnInteract();
        }
    }

    private IEnumerator RoutineRotateLever()
    {
        float angleX = trsf_rotationPoint.eulerAngles.x;
        while (trsf_rotationPoint.eulerAngles.x < 40)
        {
            angleX += Time.deltaTime * 100f;
            trsf_rotationPoint.eulerAngles = new Vector3(angleX, trsf_rotationPoint.eulerAngles.y, trsf_rotationPoint.eulerAngles.z);
            yield return null;
        }
    }
}
