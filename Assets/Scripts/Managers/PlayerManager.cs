using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerManager : MonoBehaviour
{
    private Camera cameraMain;
    RaycastHit hit1;
    private List<PlayerUnitController> selectedUnits = new List<PlayerUnitController>();
    private Interactable selectedInteractable;
    public UserInterface displayInventory;
    public UserInterface displayEquipment;
    public UserInterface displayInfo;
    [SerializeField] private RingMenuController MainMenuPrefab;
    protected RingMenuController MainMenuInstance;


    private void Awake()
    {
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        MainMenuInstance = Instantiate(MainMenuPrefab, GameObject.Find("CanvasInteractable").GetComponent<Canvas>().transform);
        // MainMenuInstance.location = transform;
        MainMenuInstance.CreatedMenu();
        MainMenuInstance.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        PlayerEventHandler.DeSelectUnits.AddListener(DeselectUnits);
        UnitFrameEventHandler.UnitFrameClicked.AddListener(UnitFrameClicked);
    }
    private void OnDisable()
    {
        UnitFrameEventHandler.UnitFrameClicked.RemoveListener(UnitFrameClicked);
        PlayerEventHandler.DeSelectUnits.RemoveListener(DeselectUnits);
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: bunu değiştirelim
        pouseText();

    }
    private void UnitFrameClicked(PlayerUnitController unit)
    {
        if (unit.isSelected() == false)
        {
            SelectUnit(unit);
        }
        else
        {
            DeselectUnit(unit);
        }
    }

    #region alakasiz

    private List<Vector3> GetPositionListAround(Vector3 startPosition, float[] ringDistanceArray, int[] ringPositionCountArray)
    {
        List<Vector3> positionList = new List<Vector3>();
        positionList.Add(startPosition);
        for (int i = 0; i < ringDistanceArray.Length; i++)
        {

            positionList.AddRange(GetPositionListAround(startPosition, ringDistanceArray[i], ringPositionCountArray[i]));
        }
        return positionList;
    }
    private List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount)
    {
        List<Vector3> positionList = new List<Vector3>();
        for (int i = 0; i < positionCount; i++)
        {
            float angle = i * (360 / positionCount);
            Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
            Vector3 position1 = startPosition + dir * distance;


            // burda position listeye eklenmeden önce yere denk egliyor mu diye bakıyoruz.
            var screenPos = cameraMain.WorldToScreenPoint(position1);
            var camRay1 = cameraMain.ScreenPointToRay(screenPos);
            if (Physics.Raycast(camRay1, out hit1))
            {

                if (hit1.transform.CompareTag("Ground"))
                {
                    positionList.Add(position1);
                }
            }

        }

        return positionList;
    }

    private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, angle, 0) * vec;
    }
    #endregion
    public void SelectUnit(PlayerUnitController unit, bool isMultiSelect = false)
    {
        if (!isMultiSelect)
        {
            DeselectUnits();
        }
        if (unit.isSelected())
        {
            selectedUnits.RemoveAll(u => u == unit);
            unit.SetSelected(false);
        }
        else
        {
            selectedUnits.Add(unit);
            unit.SetSelected(true);
        }

        //InventoryDisplay();
    }
    public void SelectUnits(PlayerUnitController[] playerUnits, bool isMultiSelection = false)
    {
        if (!isMultiSelection)
        {
            if (selectedUnits.Count > 0) DeselectUnits();
        }
        foreach (PlayerUnitController playerUnit in playerUnits)
        {
            if (playerUnit.isSelected()) return;
            SelectUnit(playerUnit, true);
        }
    }
    public void SelectInteractable(Interactable interactable)
    {
        if (selectedInteractable != null)
        {
            DeselectInteractable();

        }
        selectedInteractable = interactable;
        interactable.onCollectButtonpressed += İnteractable_onCollectButtonpressed;
        interactable.SetInteractableSelected(true);
        if (selectedUnits.Count == 0)
        {
            interactable.OpenInteractMenu(true, false);
        }
        else
        {
            interactable.OpenInteractMenu(true, true);
        }
        //ringmenu

        MainMenuInstance.gameObject.SetActive(true);

    }

    private void İnteractable_onCollectButtonpressed()
    {

        if (selectedInteractable != null)
        {
            for (int i = 0; i < selectedUnits.Count; i++)
            {

                if (selectedInteractable.getCurrentAmount() > 0)
                {
                    if (selectedUnits[i].getUnitInventory().calculateFull(selectedInteractable.item) == false)
                    {
                        if (selectedInteractable.takeInteractSlot())
                        {
                            selectedUnits[i].SetFocus(selectedInteractable.gameObject.transform);
                            selectedUnits[i].startGather(selectedInteractable);
                            DeselectUnit(selectedUnits[i]);
                            i--;
                        }
                    }

                }
            }
            //bunuda kaldırmak lazım
            DeselectInteractable();
        }

    }

    public void DeselectUnit(PlayerUnitController unit)
    {
        selectedUnits.RemoveAll(u => u == unit);
        unit.SetSelected(false);
        //InventoryDisplay();
    }

    public void DeselectUnits()
    {
        foreach (PlayerUnitController unit in selectedUnits)
        {
            unit.SetSelected(false);
        }
        selectedUnits.Clear();
        //InventoryDisplay();
    }

    public void DeselectInteractable()
    {
        if (selectedInteractable != null)
        {
            MainMenuInstance.gameObject.SetActive(false);
            selectedInteractable.onCollectButtonpressed -= İnteractable_onCollectButtonpressed;
            selectedInteractable.SetInteractableSelected(false);
            selectedInteractable.OpenInteractMenu(false, false);
            selectedInteractable = null;
        }

    }

    private void pouseText()
    {
        if (Time.timeScale == 0)
        {
            GameObject.Find("CanvasUI").transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find("CanvasUI").transform.GetChild(0).gameObject.SetActive(false);
        }

    }
    public void selectedGroundItem(groundItem groundItem)
    {
        if (selectedUnits.Count <= 0) return;
        if (selectedUnits[0].getUnitInventory().calculateFull(groundItem.item) == false)
        {
            selectedUnits[0].GetItem(groundItem.transform);
            if (selectedUnits.Count > 1)
            {
                DeselectUnit(selectedUnits[0]);
            }
        }
    }

    public void SelectedInteractable(Interactable interactable)
    {

        for (int i = 0; i < selectedUnits.Count; i++)
        {
            if (interactable.getCurrentAmount() > 0)
            {

                if (selectedUnits[i].getUnitInventory().calculateFull(interactable.item) == false)
                {
                    if (interactable.takeInteractSlot())
                    {
                        selectedUnits[i].SetFocus(interactable.gameObject.transform);
                        selectedUnits[i].startGather(interactable);
                        DeselectUnit(selectedUnits[i]);
                        i--;
                    }
                }

            }
        }

    }

    public void SelectedEnemy(EnemyUnitController enemy)
    {
        foreach (var selectableObj in selectedUnits)
        {
            selectableObj.SetNewTarget(enemy.transform);
            if (selectableObj.IsGathering())
            {
                selectableObj.stopGather();
            }
        }
    }

    public void MoveAction(Vector3 destination)
    {
        if (selectedUnits.Count <= 0) return;
        List<Vector3> targetPositionList = GetPositionListAround(destination, new float[] { 1.6f, 3.2f, 4.8f, 6.4f, 8f }, new int[] { 5, 10, 15, 20, 25 });
        List<Vector3> arrangedTargetPositionList = new List<Vector3>();
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            arrangedTargetPositionList.Add(targetPositionList[i]);
        }
        arrangedTargetPositionList.Reverse();
        var targetPositionListIndex = 0;
        foreach (var selectableObj in selectedUnits)
        {
            selectableObj.MoveUnit(arrangedTargetPositionList[targetPositionListIndex]);
            targetPositionListIndex = (targetPositionListIndex + 1) % arrangedTargetPositionList.Count;
            if (selectableObj.IsGathering())
            {
                selectableObj.stopGather();
            }

        }

    }



}
