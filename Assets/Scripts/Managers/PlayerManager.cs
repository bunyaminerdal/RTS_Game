using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{

    private Camera cameraMain;
    RaycastHit hit;
    RaycastHit hit1;
    public List<UnitController> selectedUnits = new List<UnitController>();
    Interactable selectedInteractable;
    bool isDragging = false;
    Vector3 mousePositon;
    bool isGameMenuOpened;
    public UserInterface displayInventory;
    public UserInterface displayEquipment;
    public UserInterface displayInfo;



    private void Awake()
    {
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    private void OnGUI()
    {
        if (isDragging)
        {
            var rect = ScreenHelper.GetScreenRect(mousePositon, Input.mousePosition);
            ScreenHelper.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.1f));
            ScreenHelper.DrawScreenRectBorder(rect, 1, Color.blue);
        }
    }

    // Update is called once per frame
    void Update()
    {
        pouseText();

    }

    private void InventoryDisplay()
    {
        if (selectedUnits.Count == 1)
        {

            displayInventory.SetInventory(selectedUnits[0]);
            displayEquipment.SetInventory(selectedUnits[0]);
            displayInfo.SetInventory(selectedUnits[0]);

        }
        else if (selectedUnits.Count != 1)
        {
            displayInventory.SetNullUnitInventory();
            displayEquipment.SetNullUnitInventory();
            displayInfo.SetNullUnitInventory();
        }
    }
    #region alakasiz
    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

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
    private void SelectUnit(UnitController unit, bool isMultiSelect = false)
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

        InventoryDisplay();
    }
    private void SelectInteractable(Interactable interactable)
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


    }

    private void İnteractable_onCollectButtonpressed()
    {
        if (!isGameMenuOpened)
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
    }

    public void DeselectUnit(UnitController unit)
    {
        selectedUnits.RemoveAll(u => u == unit);
        unit.SetSelected(false);
        InventoryDisplay();
    }

    public void DeselectUnits()
    {
        foreach (UnitController unit in selectedUnits)
        {
            unit.SetSelected(false);
        }
        selectedUnits.Clear();
        InventoryDisplay();
    }

    private void DeselectInteractable()
    {
        if (selectedInteractable != null)
        {
            selectedInteractable.onCollectButtonpressed -= İnteractable_onCollectButtonpressed;
            selectedInteractable.SetInteractableSelected(false);
            selectedInteractable.OpenInteractMenu(false, false);
            selectedInteractable = null;
        }

    }

    private bool IsWithinSelectionBounds(Transform transform)
    {
        if (!isDragging)
        {
            return false;
        }
        var viewportBounds = ScreenHelper.GetViewportBounds(cameraMain, mousePositon, Input.mousePosition);
        return viewportBounds.Contains(cameraMain.WorldToViewportPoint(transform.position));
    }

    public void gameMenuOpener(bool isOpened)
    {
        //TODO: MEnu açıldığında interact menu varsa kapatmalıyız
        DeselectInteractable();
        GameObject.Find("CanvasMenu").transform.GetChild(0).gameObject.SetActive(isOpened);
        isGameMenuOpened = isOpened;
    }

    private void pouseText()
    {
        if (!isGameMenuOpened && Time.timeScale == 0)
        {
            GameObject.Find("CanvasUI").transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find("CanvasUI").transform.GetChild(0).gameObject.SetActive(false);
        }

    }

    public void LeftClickDown()
    {
        if (!IsMouseOverUI())
        {
            if (selectedInteractable != null)
            {
                DeselectInteractable();
            }
            mousePositon = Input.mousePosition;
            //Create a ray from the camera to our space
            var camRay = cameraMain.ScreenPointToRay(Input.mousePosition);
            //Shoot that ray and get the hit data
            if (Physics.Raycast(camRay, out hit))
            {
                //Do something with that data 

                //Debug.Log(hit.transform.tag);
                if (hit.transform.CompareTag("PlayerUnit"))
                {
                    SelectUnit(hit.transform.GetComponent<UnitController>(), Input.GetKey(KeyCode.LeftShift));
                }
                else if (hit.transform.CompareTag("Interactable"))
                {
                    SelectInteractable(hit.transform.GetComponent<Interactable>());

                }
                else
                {
                    isDragging = true;
                }
            }
        }

    }

    public void LeftClickUp()
    {
        if (isDragging)
        {
            DeselectUnits();
            foreach (var selectableObject in FindObjectsOfType<PlayerUnitController>())
            {
                if (IsWithinSelectionBounds(selectableObject.transform))
                {
                    SelectUnit(selectableObject.gameObject.GetComponent<UnitController>(), true);
                }
            }

            isDragging = false;
        }
    }

    public void RightClickDown()
    {
        if (selectedUnits.Count > 0)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                var camRay = cameraMain.ScreenPointToRay(Input.mousePosition);
                //Shoot that ray and get the hit data
                if (Physics.Raycast(camRay, out hit))
                {

                    if (selectedInteractable != null && !hit.transform.CompareTag("Interactable"))
                    {
                        DeselectInteractable();
                    }
                    //Do something with that data                 
                    if (hit.transform.CompareTag("Ground"))
                    {
                        List<Vector3> targetPositionList = GetPositionListAround(hit.point, new float[] { 1.6f, 3.2f, 4.8f, 6.4f, 8f }, new int[] { 5, 10, 15, 20, 25 });
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
                    else if (hit.transform.CompareTag("EnemyUnit"))
                    {
                        foreach (var selectableObj in selectedUnits)
                        {
                            selectableObj.SetNewTarget(hit.transform);
                            if (selectableObj.IsGathering())
                            {
                                selectableObj.stopGather();
                            }
                        }
                    }
                    else if (hit.transform.CompareTag("Interactable"))
                    {
                        Interactable _selectedInteractable = hit.transform.GetComponent<Interactable>();
                        for (int i = 0; i < selectedUnits.Count; i++)
                        {
                            if (_selectedInteractable.getCurrentAmount() > 0)
                            {

                                if (selectedUnits[i].getUnitInventory().calculateFull(_selectedInteractable.item) == false)
                                {
                                    if (_selectedInteractable.takeInteractSlot())
                                    {
                                        selectedUnits[i].SetFocus(_selectedInteractable.gameObject.transform);
                                        selectedUnits[i].startGather(_selectedInteractable);
                                        DeselectUnit(selectedUnits[i]);
                                        i--;
                                    }
                                }

                            }
                        }
                        _selectedInteractable = null;
                    }
                    else if (hit.transform.CompareTag("Item"))
                    {
                        if (selectedUnits[0].getUnitInventory().calculateFull(hit.transform.GetComponent<groundItem>().item) == false)
                        {
                            selectedUnits[0].GetItem(hit.transform);
                            if (selectedUnits.Count > 1)
                            {
                                DeselectUnit(selectedUnits[0]);
                            }
                        }

                    }
                }
            }

        }
    }

}
