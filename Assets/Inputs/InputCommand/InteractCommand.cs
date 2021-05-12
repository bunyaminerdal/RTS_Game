using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractCommand : Command
{
    private Camera cameraMain;    
    private void Awake()
    {        
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    public override void ExecuteWithVector2(Vector2 vector2)
    {
        if (!IsMouseOverUI())
        {           
            var camRay = cameraMain.ScreenPointToRay(vector2);
            RaycastHit hit;
            //Shoot that ray and get the hit data
            if (Physics.Raycast(camRay, out hit))
            {
                //Do something with that data herbiri için ayrý state e girecek
                hit.transform.TryGetComponent<EnemyUnitController>(out EnemyUnitController enemyUnit);
                //Debug.Log(enemyUnit);
                hit.transform.TryGetComponent<PlayerUnitController>(out PlayerUnitController playerUnit);
                //Debug.Log(playerUnit);
                hit.transform.TryGetComponent<Interactable>(out Interactable interact);
                //Debug.Log(interact);
                hit.transform.TryGetComponent<GroundIneraction>(out GroundIneraction ground);
                //Debug.Log(ground);
                hit.transform.TryGetComponent<groundItem>(out groundItem groundItem);
                //Debug.Log(groundItem);
            }
        }
    }
    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
