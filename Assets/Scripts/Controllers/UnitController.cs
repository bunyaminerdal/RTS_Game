using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    private UnitBoxController unitBoxController;
    private SkinnedMeshRenderer newMeshHelmet;
    private SkinnedMeshRenderer newMeshChest;
    private SkinnedMeshRenderer newMeshWeapon;
    public SkinnedMeshRenderer TargetMesh;
    public NavMeshAgent navAgent;
    private Transform currentTarget;
    private Transform currentFocus;
    private Transform currentTargetItem;
    private float attackTimer;
    private float gatherTimer;
    Animator animator;
    public string unitName;
    public string unitType;

    public Vector3 unitDestination;
    public Interactable unitInteract;

    public UnitStats unitStats;
    private bool isGathering;


    public GameObject clickMarker;
    public Interactable interact;
    public Transform clickMarkerTransform;
    [SerializeField]
    private GameObject selectionMarker;
    private LineRenderer myLineRenderer;
    private bool isUnitSelected;
    private UnitInventory unitInventory = new UnitInventory(6);
    public UnitInventory unitInventoryStart = new UnitInventory(6);
    private UnitInventory unitEqInventory = new UnitInventory(3);
    public UnitInventory unitEqInventoryStart = new UnitInventory(3);

    private float distance;

    private float currentHealth;
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }

    public GameObject healthBarPrefab;
    private GameObject healthBar;
    private bool isDead;
    //TODO: Şuan elle prefab içine girdim ama otomatik yapsam daha güzel olur
    public Attribute[] attributes;

    public RenderTexture copyRenderTexture;
    private RenderTexture unitRenderTexture;
    private Camera unitCam;
    public Texture2D unitTexture;
    private Rect rect;
    private void Start()
    {
        unitBoxController = FindObjectOfType<UnitBoxController>();
        unitCam = GetComponentInChildren<Camera>();
        rect = new Rect(0, 0, 100, 100);
        unitRenderTexture = new RenderTexture(copyRenderTexture);
        unitTexture = new Texture2D(100, 100, TextureFormat.RGBAFloat, false);
        StartCoroutine(UnitTextureRender());

        healthBar = Instantiate(healthBarPrefab, transform);

        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        //clickMarker
        myLineRenderer = GetComponent<LineRenderer>();
        myLineRenderer.startWidth = 0.1f;
        myLineRenderer.endWidth = 0.1f;
        myLineRenderer.positionCount = 0;

        if (unitDestination != transform.position)
        {
            MoveUnit(unitDestination);
        }

        if (unitInteract != null)
        {
            SetFocus(unitInteract.transform);
            unitInteract.takedInteractSlot();
            startGather(unitInteract);
        }

        for (int i = 0; i < unitEqInventory.Container.Slots.Length; i++)
        {
            unitEqInventory.Container.Slots[i].AllowedItems = new ItemType[1];
            if (i == 0)
            {
                unitEqInventory.Container.Slots[i].AllowedItems[0] = ItemType.Chest;
            }
            else if (i == 1)
            {
                unitEqInventory.Container.Slots[i].AllowedItems[0] = ItemType.Helmet;
            }
            else if (i == 2)
            {
                unitEqInventory.Container.Slots[i].AllowedItems[0] = ItemType.Weapon;
            }

        }

        //bunun save de çalışabilmesi için inventory oluşturulurken yapılması lazım.
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }
        CurrentHealth = unitStats.health;
        //attributelar içinde name ve status ayarlama
        attributes[0].stringValue.SetModifier(unitName);
        attributes[1].stringValue.SetModifier("Unit Status");
        attributes[2].value.BaseValue = (int)unitStats.health;
        attributes[3].value.BaseValue = unitStats.armour;
        attributes[4].value.BaseValue = unitStats.attackSpeed;
        attributes[5].value.BaseValue = unitStats.attackDamage;
        attributes[6].value.BaseValue = unitStats.attackRange;
        for (int i = 0; i < unitEqInventory.GetSlots.Length; i++)
        {
            unitEqInventory.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            unitEqInventory.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }
        if (unitInventoryStart != null)
        {
            SetInventory(unitInventoryStart);
            unitInventoryStart = null;
        }
        if (unitEqInventoryStart != null)
        {
            SetEqInventory(unitEqInventoryStart);
            unitEqInventoryStart = null;
        }
        gatherTimer = unitStats.gatheringSpeed;
    }
    public void OnBeforeSlotUpdate(InventorySlot _slot)
    {
        if (_slot.item == null)
            return;

        for (int i = 0; i < _slot.item.buffs.Length; i++)
        {
            for (int j = 0; j < attributes.Length; j++)
            {
                if (attributes[j].type == _slot.item.buffs[i].attribute)
                {
                    attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                }
            }
        }
        if (_slot.item.unitDisplay != null)
        {
            switch (_slot.AllowedItems[0])
            {
                case ItemType.Helmet:
                    if (newMeshHelmet.gameObject != null)
                    {
                        Destroy(newMeshHelmet.gameObject);

                    }
                    break;
                case ItemType.Chest:
                    if (newMeshChest.gameObject != null)
                    {
                        Destroy(newMeshChest.gameObject);

                    }
                    break;
                case ItemType.Weapon:
                    if (newMeshWeapon.gameObject != null)
                    {
                        Destroy(newMeshWeapon.gameObject);

                    }
                    break;

                default:
                    break;
            }
        }
    }
    public void OnAfterSlotUpdate(InventorySlot _slot)
    {

        if (_slot.item == null)
        {
            StartCoroutine(UnitTextureRender());
            return;
        }

        for (int i = 0; i < _slot.item.buffs.Length; i++)
        {
            for (int j = 0; j < attributes.Length; j++)
            {
                if (attributes[j].type == _slot.item.buffs[i].attribute)
                {
                    attributes[j].value.AddModifier(_slot.item.buffs[i]);
                }
            }
        }
        if (_slot.item.unitDisplay != null)
        {
            switch (_slot.AllowedItems[0])
            {
                case ItemType.Helmet:
                    newMeshHelmet = Instantiate<SkinnedMeshRenderer>(_slot.item.unitDisplay);
                    newMeshHelmet.transform.parent = TargetMesh.transform;
                    newMeshHelmet.bones = TargetMesh.bones;
                    newMeshHelmet.rootBone = TargetMesh.bones[4];
                    break;
                case ItemType.Chest:
                    newMeshChest = Instantiate<SkinnedMeshRenderer>(_slot.item.unitDisplay);
                    newMeshChest.transform.parent = TargetMesh.transform;
                    newMeshChest.bones = TargetMesh.bones;
                    newMeshChest.rootBone = TargetMesh.bones[2];
                    break;
                case ItemType.Weapon:
                    newMeshWeapon = Instantiate<SkinnedMeshRenderer>(_slot.item.unitDisplay);
                    newMeshWeapon.transform.parent = TargetMesh.transform;
                    newMeshWeapon.bones = TargetMesh.bones;
                    newMeshWeapon.rootBone = TargetMesh.bones[14];

                    break;

                default:
                    break;
            }
            StartCoroutine(UnitTextureRender());
        }

    }
    IEnumerator UnitTextureRender()
    {
        yield return null;
        if (unitCam != null)
        {
            unitCam.targetTexture = unitRenderTexture;
            unitCam.Render();
            RenderTexture.active = unitRenderTexture;
            unitTexture.ReadPixels(rect, 0, 0);
            unitTexture.Apply();
            unitCam.targetTexture = null;
            RenderTexture.active = null;
            unitCam.gameObject.SetActive(false);
            unitBoxController.onItemChange(GetComponent<PlayerUnitController>(), unitTexture);
        }

    }

    private void Update()
    {
        if (isDead)
        {
            if (attributes[1].stringValue.ModifiedValue != "Dead")
                attributes[1].stringValue.SetModifier("Dead");
            animator.SetBool("isDeath", true);
            return;
        }
        //click marker
        if (navAgent.remainingDistance < navAgent.stoppingDistance)
        {
            if (clickMarker.activeSelf)
            {
                clickMarker.SetActive(false);
                clickMarker.transform.position = transform.position;
                clickMarker.transform.SetParent(transform);
                myLineRenderer.positionCount = 0;
            }
            if (attributes[1].stringValue.ModifiedValue != "Idle")
            {
                attributes[1].stringValue.SetModifier("Idle");
                animator.SetFloat("attackTimer", 1);
                animator.SetBool("isRunning", false);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isGathering", false);
                animator.SetBool("isShooting", false);
            }


        }
        else if (navAgent.hasPath)
        {
            if (isUnitSelected)
            {
                DrawPath();
            }
            else
            {
                if (clickMarker.activeSelf)
                {
                    //click marker
                    clickMarker.SetActive(false);
                    clickMarker.transform.position = transform.position;
                    clickMarker.transform.SetParent(transform);
                    myLineRenderer.positionCount = 0;
                }
            }
            if (attributes[1].stringValue.ModifiedValue != "Running")
            {
                attributes[1].stringValue.SetModifier("Running");
                animator.SetFloat("attackTimer", 1);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isGathering", false);
                animator.SetBool("isShooting", false);
                animator.SetBool("isRunning", true);
            }

        }

        if (currentTarget != null)
        {

            distance = (transform.position - currentTarget.position).magnitude;

            FaceTarget(currentTarget);
            if (distance <= attributes[6].value.ModifiedValue)
            {
                if (attributes[6].value.ModifiedValue <= 2)
                {
                    animator.SetBool("isAttacking", true);
                }
                else
                {
                    animator.SetBool("isShooting", true);
                }

                animator.SetFloat("attackTimer", attackTimer);

                navAgent.destination = transform.position;

                attackTimer -= Time.deltaTime;
                Attack();
                if (attributes[1].stringValue.ModifiedValue != "Attacking")
                    attributes[1].stringValue.SetModifier("Attacking");

            }
            else
            {
                navAgent.destination = currentTarget.position;
            }
        }
        if (currentFocus != null)
        {
            if (navAgent.destination != currentFocus.Find("InteractionPoint").gameObject.transform.position)
            {
                navAgent.destination = currentFocus.Find("InteractionPoint").gameObject.transform.position;
                distance = (transform.position - currentFocus.Find("InteractionPoint").gameObject.transform.position).magnitude;
            }
            FaceTarget(currentFocus);
            if (distance <= 2f)
            {
                if (isGathering)
                {
                    gatherTimer -= Time.deltaTime;
                    Gather();
                    if (attributes[1].stringValue.ModifiedValue != "Gathering")
                    {
                        attributes[1].stringValue.SetModifier("Gathering");
                        animator.SetBool("isGathering", true);
                    }
                }

            }


        }

        if (currentTargetItem != null)
        {
            if (navAgent.destination != currentTargetItem.position)
            {
                navAgent.destination = currentTargetItem.position;
                distance = (transform.position - currentTargetItem.position).magnitude;
            }

            if (distance <= 2f)
            {
                var item = currentTargetItem.GetComponent<groundItem>();
                unitInventory.AddItem(item.item, 1);
                Destroy(item.gameObject);
            }

        }
    }

    public void MoveUnit(Vector3 dest)
    {
        currentTarget = null;
        currentFocus = null;
        currentTargetItem = null;
        navAgent.stoppingDistance = 1f;
        navAgent.destination = dest;
        navAgent.updateRotation = true;
    }

    public void SetSelected(bool isSelected)
    {

        selectionMarker.SetActive(isSelected);

        isUnitSelected = isSelected;
        unitBoxController.onUnitSelected(GetComponent<PlayerUnitController>(), isSelected);
    }

    public bool isSelected()
    {
        return isUnitSelected;
    }

    public void SetNewTarget(Transform enemy)
    {
        currentTarget = enemy;
        currentFocus = null;
        currentTargetItem = null;
        navAgent.stoppingDistance = 2f;
        navAgent.updateRotation = false;
        attackTimer = attributes[4].value.ModifiedValue;
    }

    public void SetFocus(Transform newFocus)
    {
        currentTarget = null;
        currentTargetItem = null;
        currentFocus = newFocus;
        navAgent.stoppingDistance = 1.8f;
        navAgent.updateRotation = false; //true olursa gelince kafasını çeviriyor, false olursa gelirken dönüyor
        gatherTimer = unitStats.gatheringSpeed;
    }

    public void Attack()
    {

        if (attackTimer <= 0)
        {
            RTSGameManager.UnitTakeDamage(this, currentTarget.GetComponent<UnitController>());
            attackTimer = attributes[4].value.ModifiedValue;
        }
    }

    public void Gather()
    {
        if (currentFocus.GetComponent<Interactable>().getCurrentAmount() <= 0)
        {
            stopGather();
        }
        else
        {
            if (newMeshWeapon != null)
            {
                newMeshWeapon.gameObject.SetActive(false);

            }
            if (gatherTimer <= 0)
            {
                RTSGameManager.UnitGather(this, currentFocus.GetComponent<Interactable>());
                gatherTimer = unitStats.gatheringSpeed;
            }
        }

    }

    public void TakeDamage(UnitController enemy, float damage)
    {
        ModifyHealth(damage);
        if (CurrentHealth <= 0)
        {
            enemy.currentTarget = null;
            isDead = true;
            Destroy(transform.gameObject, 5);
        }
    }

    public event Action<float> OnHealthPctChanged = delegate { };

    public void ModifyHealth(float amount)
    {
        CurrentHealth -= amount;
        float currentHealthPct = CurrentHealth / (float)attributes[2].value.ModifiedValue;
        OnHealthPctChanged(currentHealthPct);
    }


    void FaceTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void stopGather()
    {
        interact.giveInteractSlot();
        interact = null;
        isGathering = false;
        if (newMeshWeapon != null)
        {
            newMeshWeapon.gameObject.SetActive(true);

        }
    }
    public void startGather(Interactable _interact)
    {
        if (interact != null)
        {
            interact.giveInteractSlot();
            interact = null;
        }
        interact = _interact;
        isGathering = true;


    }

    public bool IsGathering()
    {
        return isGathering;
    }

    //clickMarker
    void DrawPath()
    {
        myLineRenderer.positionCount = navAgent.path.corners.Length;
        myLineRenderer.SetPosition(0, transform.position);
        if (navAgent.path.corners.Length < 2)
        {
            return;
        }
        Vector3 pointPos = Vector3.zero;
        for (int i = 0; i < navAgent.path.corners.Length; i++)
        {
            pointPos = new Vector3(navAgent.path.corners[i].x, navAgent.path.corners[i].y, navAgent.path.corners[i].z);
            myLineRenderer.SetPosition(i, pointPos);
        }
        clickMarker.transform.position = pointPos;
        if (!clickMarker.activeSelf)
        {
            //click marker
            clickMarker.transform.SetParent(clickMarkerTransform);
            clickMarker.SetActive(true);

        }

    }

    public void GetItem(Transform item)
    {
        currentTarget = null;
        currentTargetItem = item;
        currentFocus = null;
        navAgent.stoppingDistance = 2f;
        navAgent.updateRotation = true;
    }

    public void addItemToInventory(Item item)
    {
        if (isGathering)
        {
            unitInventory.AddItem(item, 1);
        }

    }

    public UnitInventory getUnitInventory()
    {
        return unitInventory;
    }

    public void SetInventory(UnitInventory inventory)
    {
        unitInventory = inventory;
    }
    public UnitInventory getUnitEqInventory()
    {
        return unitEqInventory;
    }

    public void SetEqInventory(UnitInventory inventory)
    {
        for (int i = 0; i < inventory.Container.Slots.Length; i++)
        {
            unitEqInventory.GetSlots[i].updateSlot(inventory.Container.Slots[i].ID, inventory.Container.Slots[i].item, inventory.Container.Slots[i].amount);
        }
    }

    //unitin modifierları update olunca çalışacak
    public void AttributeModified(Attribute _attribute)
    {
        if (_attribute.type == Attributes.Status)
        {
            Debug.Log("Name was changed");
        }

    }


}

