using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMarkerCollector : MonoBehaviour
{
    private void OnEnable()
    {
        SaveLoadHandlers.ClearClickMarkers.AddListener(GetClickMarkers);
    }
    private void OnDisable()
    {
        SaveLoadHandlers.ClearClickMarkers.RemoveListener(GetClickMarkers);
    }
    private void GetClickMarkers()
    {
        ClickMarker[] clickMarkers = transform.GetComponentsInChildren<ClickMarker>();
        foreach (ClickMarker clickMarker in clickMarkers)
        {
            Destroy(clickMarker.gameObject);
        }
    }
}
