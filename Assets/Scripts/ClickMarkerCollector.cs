using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMarkerCollector : MonoBehaviour
{
    private ClickMarker[] clickMarkers;
    private void OnEnable()
    {
        SaveLoadHandlers.ClickMarkerCollectorGetMarkers.AddListener(GetClickMarkers);
    }
    private void OnDisable()
    {
        SaveLoadHandlers.ClickMarkerCollectorGetMarkers.RemoveListener(GetClickMarkers);
    }
    private void GetClickMarkers()
    {
        clickMarkers = transform.GetComponentsInChildren<ClickMarker>();
        SaveLoadHandlers.ClickMarkerCollectorSetMarkers?.Invoke(clickMarkers);
    }
}
