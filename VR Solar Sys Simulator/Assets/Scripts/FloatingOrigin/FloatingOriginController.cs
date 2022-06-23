using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[DefaultExecutionOrder(1000)]
public class FloatingOriginController : MonoBehaviour
{
    [SerializeField]
    private OriginShiftEventChannelSO OriginShiftEventChannel;

    [SerializeField]
    private Transform PlayerTransform;

    [SerializeField]
    private GameObject SystemObject;

    private SphereCollider _sphereCollider;
    private float _radius;

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.enabled = false;
        _radius = _sphereCollider.radius;
    }

    private void FixedUpdate()
    {
        var referencePosition = PlayerTransform.position;

        if (referencePosition.magnitude >= _radius)
        {
            //Origin Shift
            OriginShiftEventChannel.Raise(-referencePosition);
            Debug.Log("FloatingOriginController.cs raised Origin Shift Event");
        }
    }
}
