using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class TrailRendererOriginShiftController : MonoBehaviour
{
    [SerializeField]
    private OriginShiftEventChannelSO OriginShiftEventChannel;

    [SerializeField]
    private TrailRenderer[] TrailRenderers;

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
        foreach (var trailRenderer in TrailRenderers)
        {
            OriginShift(trailRenderer, offset);
        }
    }

    private void OriginShift(TrailRenderer trailRenderer, Vector3 offset)
    {
        var positions = new Vector3[trailRenderer.positionCount];
        trailRenderer.GetPositions(positions);

        for (var i = 0; i < positions.Length; i++)
        {
            positions[i] += offset;
        }

        trailRenderer.SetPositions(positions);
    }
}
