using UnityEngine;
public class TakeGroundItemState : IState
{
    private readonly UnitController unit;
    private readonly Animator animator;
    private GroundItem groundItem;
    public TakeGroundItemState(UnitController _unit, Animator _animator)
    {
        unit = _unit;
        animator = _animator;
    }
    public void OnEnter()
    {
        groundItem = unit.currentTargetItem.GetComponent<GroundItem>();
        unit.currentTargetItem = null;
    }

    public void OnExit()
    {
        unit.addItemToInventory(groundItem.item);
        unit.DestroyGameObject(groundItem.gameObject);
    }

    public void Tick()
    {
        //throw new System.NotImplementedException();
    }
}
