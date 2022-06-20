using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class TrailRendererOriginShiftController : MonoBehaviour
{
    [SerializeField]
    private OriginShiftEventChannelSO OriginShiftEventChannel;

    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
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
        var positions = new Vector3[_trailRenderer.positionCount];
        _trailRenderer.GetPositions(positions);

        for (var i = 0; i < positions.Length; i++)
        {
            positions[i] += offset;
        }

        _trailRenderer.SetPositions(positions);
    }
}
