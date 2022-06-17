using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Origin Shift", order = 0)]
public class OriginShiftEventChannelSO : ScriptableObject
{
    public delegate void OriginShift(Vector3 offset);
    public event OriginShift Raised = delegate { };

    public void Raise(Vector3 offset)
    {
        Raised.Invoke(offset);
    }
    
}
