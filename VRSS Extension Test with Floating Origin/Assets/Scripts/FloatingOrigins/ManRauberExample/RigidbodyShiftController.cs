using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyShiftController : MonoBehaviour
{
    [SerializeField]
    private OriginShiftEventChannelSO OriginShiftEventChannel;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

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
        _rigidbody.position += offset;
    }
}
