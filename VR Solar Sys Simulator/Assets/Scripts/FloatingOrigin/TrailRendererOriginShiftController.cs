using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(TrailRenderer))]
public class TrailRendererOriginShiftController : MonoBehaviour
{
    [SerializeField]
    private OriginShiftEventChannelSO OriginShiftEventChannel;

    
    private TrailRenderer[] TrailRenderers;
    [SerializeField]
    private List<TrailRenderer> trailsList;

    private void Update()
    {
        trailsList = new List<TrailRenderer>();
        for (int i = 0; i < gameObject.GetComponent<SimulationSettings>().celestials.Length; i++)
        {
            trailsList.Add(gameObject.GetComponent<SimulationSettings>().celestials[i].GetComponentInChildren<TrailRenderer>());
        }
    }

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
        foreach (var trailRenderer in trailsList)
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