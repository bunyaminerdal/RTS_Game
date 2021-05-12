using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCommand : Command
{
    [SerializeField]
    private float zoomSpeed = 80;
    [SerializeField]
    private float minZoom = 0;
    [SerializeField]
    private float maxZoom = 30;
    [SerializeField]
    private float lerpTime = 10;

    private float zoomAmount;
    public override void ExecuteWithFloat(float value)
    {
        zoomAmount = Mathf.Clamp(zoomAmount -(value / zoomSpeed), minZoom, maxZoom) ;
        transform.position = Vector3.Lerp(transform.position,new Vector3(0, zoomAmount, 0),lerpTime);
    }
}
