
using UnityEngine.Events;

public static class MenuEventHandler
{
    public static UnityEvent ResumeButtonClicked = new UnityEvent();
    public static UnityEvent<MenuType> GameMenuClicked = new UnityEvent<MenuType>();
    public static UnityEvent<MenuType> CurrentMenuChanged = new UnityEvent<MenuType>();
    public static UnityEvent QuickSaveClicked = new UnityEvent();
    public static UnityEvent QuickLoadClicked = new UnityEvent();
}
