
using UnityEngine.Events;

public static class SaveLoadHandlers 
{
    public static UnityEvent<float, float, float> PlayerManagerTransform = new UnityEvent<float, float, float>();
    public static UnityEvent<float> PlayerManagerRotationY = new UnityEvent<float>();
    public static UnityEvent<float> VirtualCamOffset = new UnityEvent<float>();
    public static UnityEvent<float, float, float> PlayerManagerTransformLoad = new UnityEvent<float, float, float>();
    public static UnityEvent<float> PlayerManagerRotationYLoad = new UnityEvent<float>();
    public static UnityEvent<float> VirtualCamOffsetLoad = new UnityEvent<float>();

    public static UnityEvent playerUnitCollectorGetUnits = new UnityEvent();
    public static UnityEvent<PlayerUnitController[]> playerUnitCollectorSetUnits = new UnityEvent<PlayerUnitController[]>();

    public static UnityEvent interactableCollectorGetInteracts = new UnityEvent();
    public static UnityEvent<Interactable[]> interactableCollectorSetInteracts = new UnityEvent<Interactable[]>();

    public static UnityEvent ClickMarkerCollectorGetMarkers = new UnityEvent();
    public static UnityEvent<ClickMarker[]> ClickMarkerCollectorSetMarkers = new UnityEvent<ClickMarker[]>();

    public static UnityEvent<InteractableBasics[]> SetInteractableBasicsLoadingForCreate = new UnityEvent<InteractableBasics[]>();
    public static UnityEvent<UnitBasics[]> SetUnitBasicsLoadingForCreate = new UnityEvent<UnitBasics[]>();
    public static UnityEvent<InventoryBasics[]> SetInventoryBasicsLoadingForCreate = new UnityEvent<InventoryBasics[]>();
    public static UnityEvent<ItemBasics[]> SetItemBasicsLoadingForCreate = new UnityEvent<ItemBasics[]>();
    public static UnityEvent<ItemAttributeBasics[]> SetItemAttBasicsLoadingForCreate = new UnityEvent<ItemAttributeBasics[]>();
    public static UnityEvent CreatorFuncEvent = new UnityEvent();

    public static UnityEvent UnitFrameClearBeforeUnitCreated = new UnityEvent();
    public static UnityEvent<PlayerUnitController> UnitFrameClearAfterUnitCreated = new UnityEvent<PlayerUnitController>();

}
