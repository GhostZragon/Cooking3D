using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHoldWorldPosition : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] private Vector3 StandPosition;
    [SerializeField] private bool getPosWhenStart = false;
    [SerializeField] private Vector3 offsetScreen;

    private void Start()
    {
        mainCam = Camera.main;
    }

    public void SetStandPosition(Vector3 position)
    {
        StandPosition = position;
    }
 
    private int interval = 3;
    void Update()
    {
        if (Time.frameCount % interval == 0)
        {
            transform.position = mainCam.WorldToScreenPoint(StandPosition + offsetScreen);

        }
    }
}
