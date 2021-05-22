
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public static List<Interactable> AllInteractables { get; private set; }
    public Action OnCollectButtonpressed { get; internal set; }

    public Transform InteractableTransform;

    public float radius = 3f;

    //bool isFocus = false;
    public GameObject interactMenu;

    protected GameObject Menu;
    protected Button yourButton;

    public CollectableObject collectable;
    private Text[] collectableTexts;
    private float maxAmount;
    private float minAmount;
    public float CurrentAmount;
    private bool isDepleted;
    public float respawnTime;
    private bool isUnitSelected;
    private int interactSlot;
    private int maxInteractSlot;
    public Item item;
    private Canvas interactCanvas;

    public string interactName;
    public string interactType;
    private Camera mainCamera;

    //Düğme basıldığında trigerlanacak

    private void OnEnable()
    {
        if (AllInteractables == null)
            AllInteractables = new List<Interactable>();
        AllInteractables.Add(this);
    }

    private void OnDisable()
    {
        AllInteractables.Remove(this);
    }
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        interactCanvas = GameObject.Find("CanvasInteractable").GetComponent<Canvas>();
        Menu = Instantiate(interactMenu, Vector3.zero, Quaternion.identity, interactCanvas.transform);
        Menu.name = interactName + "_Menu";
        Menu.SetActive(false);
        maxAmount = collectable.maxAmount;
        minAmount = collectable.minAmount;
        interactSlot = collectable.interactSlot;
        maxInteractSlot = collectable.interactSlot;
        item = new Item(collectable.item);
        collectableTexts = Menu.GetComponentsInChildren<Text>();
        yourButton = Menu.GetComponentInChildren<Button>();
        yourButton.onClick.AddListener(TaskOnClick);
    }

    void Update()
    {
        if (respawnTime > 0)
        {
            isDepleted = true;
            respawnTime -= Time.deltaTime;
            if (respawnTime < 0)
            {
                Debug.Log("respawned");
                CurrentAmount = maxAmount;
                isDepleted = false;
            }
        }
        createMenu();

    }

    private void createMenu()
    {
        if (Menu.activeSelf)
        {
            Menu.transform.position = mainCamera.WorldToScreenPoint(transform.position);

            foreach (var child in collectableTexts)
            {
                if (child.name == "Amount")
                {
                    child.text = "Amount: " + CurrentAmount.ToString() + " / " + maxAmount.ToString();
                }
                else if (child.name == "Slot")
                {
                    child.text = "Slot: " + interactSlot.ToString() + " / " + maxInteractSlot.ToString();
                }
            }
            if (!isDepleted)
            {
                if (isUnitSelected)
                {
                    if (interactSlot > 0)
                    {
                        yourButton.interactable = true;
                    }
                    else
                    {
                        yourButton.interactable = false;
                    }

                }

            }

        }
    }

    public void SetInteractableSelected(bool isSelected)
    {
        transform.Find("InteractHighlight").gameObject.SetActive(isSelected);

    }

    public void OpenInteractMenu(bool isSelected, bool _isUnitSelected)
    {

        if (!isSelected)
        {

            Menu.SetActive(false);
            return;
        }
        isUnitSelected = _isUnitSelected;

        Menu.SetActive(true);
        foreach (var child in collectableTexts)
        {
            if (child.name == "Title")
            {
                child.text = collectable.collectablename;
            }
            else if (child.name == "Description")
            {
                child.text = collectable.description;

            }

        }


        if (!isDepleted)
        {
            if (isUnitSelected)
            {
                yourButton.interactable = true;
            }
            else
            {
                yourButton.interactable = false;
            }

        }
        else
        {
            yourButton.interactable = false;
        }



    }



    void TaskOnClick()
    {
        OnCollectButtonpressed?.Invoke();
    }

    public float getCurrentAmount()
    {
        return CurrentAmount;
    }

    public void setCurrentAmount()
    {
        if (CurrentAmount > minAmount)
        {
            CurrentAmount--;
            if (CurrentAmount == minAmount)
            {
                respawnTime = collectable.respawnTime;
            }
        }

    }


    public bool takeInteractSlot()
    {
        if (interactSlot == 0)
        {
            return false;
        }
        else if (interactSlot > 0)
        {
            takedInteractSlot();
            return true;
        }
        else
        {
            return false;
        }

    }

    public void giveInteractSlot()
    {
        interactSlot += 1;
    }

    public void takedInteractSlot()
    {
        interactSlot -= 1;
    }

}
