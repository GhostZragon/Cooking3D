using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHoldWorldPosition : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] private Vector3 StandPosition;
    private void Start()
    {
        mainCam = Camera.main;
    }

    public void SetStandPosition(Vector3 position)
    {
        StandPosition = position;
    }
    private void Update()
    {
        transform.position = mainCam.WorldToScreenPoint(StandPosition);
    }

}
