using UnityEngine.Events;

public static class UnitFrameEventHandler
{
    public static UnityEvent<PlayerUnitController> UnitFrameClicked = new UnityEvent<PlayerUnitController>();
}
