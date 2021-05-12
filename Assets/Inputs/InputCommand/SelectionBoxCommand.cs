using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionBoxCommand : Command
{

    private Camera cameraMain;
    private Vector3 mouseStartPositon;
    private Vector3 mouseEndPosition;
    private bool isDragging;

    private void Awake()
    {
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    public override void ExecuteWithVector2(Vector2 vector2)
    {
        if (isDragging) return;
        if (!IsMouseOverUI())
        {
            isDragging = false;
            var camRay = cameraMain.ScreenPointToRay(vector2);
                RaycastHit hit;
                
                if (Physics.Raycast(camRay, out hit))
                {
                    
                    hit.transform.TryGetComponent<GroundIneraction>(out GroundIneraction ground);
                                        
                    if (ground == null) return;
                    
                    mouseEndPosition = vector2;
                    mouseStartPositon = vector2;
                    isDragging = true;
                
                }
            
        }
    }

    public override void EndWithVector2(Vector2 vector2)
    {
        if (!isDragging) return;
        var viewportBounds = ScreenHelper.GetViewportBounds(cameraMain, mouseStartPositon, vector2);        
        //DeselectUnits();
        foreach (var selectableObject in FindObjectsOfType<PlayerUnitController>())
        {
            if (viewportBounds.Contains(cameraMain.WorldToViewportPoint(selectableObject.transform.position)))
            {
                ///SelectUnit(selectableObject.gameObject.GetComponent<UnitController>(), true);
                Debug.Log(selectableObject.unitName);
            }
        }
        isDragging = false;
        
    }
    private void OnGUI()
    {
        if (!isDragging) return;
        
            var rect = ScreenHelper.GetScreenRect(mouseStartPositon, mouseEndPosition);
            ScreenHelper.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.1f));
            ScreenHelper.DrawScreenRectBorder(rect, 1, Color.blue);
        
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public override void DragWithVector2(Vector2 vector2)
    {
        if (!isDragging) return;
        mouseEndPosition = vector2;
    }
    
}
