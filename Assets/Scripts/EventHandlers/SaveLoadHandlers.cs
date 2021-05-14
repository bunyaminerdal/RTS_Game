
using UnityEngine.Events;

public static class SaveLoadHandlers 
{
    public static UnityEvent<float, float, float> PlayerManagerTransform = new UnityEvent<float, float, float>();
    public static UnityEvent<float> PlayerManagerRotationY = new UnityEvent<float>();
    public static UnityEvent<float> VirtualCamOffset = new UnityEvent<float>();
    public static UnityEvent<float, float, float> PlayerManagerTransformLoad = new UnityEvent<float, float, float>();
    public static UnityEvent<float> PlayerManagerRotationYLoad = new UnityEvent<float>();
    public static UnityEvent<float> VirtualCamOffsetLoad = new UnityEvent<float>();
}
