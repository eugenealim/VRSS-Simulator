using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformOriginShiftController : MonoBehaviour
{
    [SerializeField]
    private OriginShiftEventChannelSO OriginShiftEventChannel;

    private void OnEnable()
    {
        OriginShiftEventChannel.Raised += Shift;
    }

    private void OnDisable()
    {
        OriginShiftEventChannel.Raised -= Shift;
    }

    private void Shift(Vector3 offset)
    {
        transform.position += offset;
    }
}
