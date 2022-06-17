using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginShiftController : MonoBehaviour
{
    [SerializeField]
    private OriginShiftEventChannelSO OriginShiftEventChannel;

    private void OnEnable()
    {
        OriginShiftEventChannel.Raised += OriginShift;
    }

    private void OnDisable()
    {
        OriginShiftEventChannel.Raised -= OriginShift;
    }

    private void OriginShift(Vector3 offset)
    {
        transform.position += offset;
    }
}
