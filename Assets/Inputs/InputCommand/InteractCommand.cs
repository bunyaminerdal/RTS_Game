using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractCommand : Command
{
    private PlayerManager playerManager;
    private Camera cameraMain;
    private void Awake()
    {
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerManager = GetComponent<PlayerManager>();
    }
    public override void ExecuteWithVector2(Vector2 vector2, bool isMultiSelection)
    {
        if (!IsMouseOverUI())
        {
            var camRay = cameraMain.ScreenPointToRay(vector2);
            RaycastHit hit;
            //Shoot that ray and get the hit data
            if (Physics.Raycast(camRay, out hit))
            {
                //Do something with that data herbiri i�in ayr� state e girecek
                hit.transform.TryGetComponent<EnemyUnitController>(out EnemyUnitController enemyUnit);
                if (enemyUnit != null)
                {
                    playerManager.SelectedEnemy(enemyUnit);
                }
                //Debug.Log(enemyUnit);

                //ekip arkadaşımızla nasıl interaction olacağı belli değil
                hit.transform.TryGetComponent<PlayerUnitController>(out PlayerUnitController playerUnit);
                //Debug.Log(playerUnit);
                hit.transform.TryGetComponent<Interactable>(out Interactable interact);
                if (interact != null)
                {
                    playerManager.SelectedInteractable(interact);
                }
                //Debug.Log(interact);
                hit.transform.TryGetComponent<GroundIneraction>(out GroundIneraction ground);
                if (ground != null)
                {
                    playerManager.MoveAction(hit.point);
                }
                //Debug.Log(ground);
                hit.transform.TryGetComponent<groundItem>(out groundItem groundItem);
                if (groundItem != null)
                {
                    playerManager.selectedGroundItem(groundItem);
                }
            }
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

}
