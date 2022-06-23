using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class TrailRendererOriginShiftController : MonoBehaviour
{
    [SerializeField]
    private OriginShiftEventChannelSO OriginShiftEventChannel;

    [SerializeField]
    //private List<TrailRenderer> TrailRenderers;
    private TrailRenderer trailRenderer;

    private void Start()
    {
        //TrailRenderers.Add(gameObject.GetComponent<TrailRenderer>());
        FindTrailRenderer(gameObject);
    }

    public void FindTrailRenderer(GameObject gameObj)
    {
        //TrailRenderers.Add(gameObject.GetComponent<TrailRenderer>());
        trailRenderer = gameObj.GetComponent<TrailRenderer>();
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
        var positions = new Vector3[trailRenderer.positionCount];
        trailRenderer.GetPositions(positions);

        for (var i = 0; i < positions.Length; i++)
        {
            positions[i] += offset;
        }

        trailRenderer.SetPositions(positions);
    }

    //private void OriginShift(Vector3 offset)
    //{
    //    foreach (var trailRenderer in TrailRenderers)
    //    {
    //        OriginShift(trailRenderer, offset);
    //    }
    //}

    //private void OriginShift(TrailRenderer trailRenderer, Vector3 offset)
    //{
    //    var positions = new Vector3[trailRenderer.positionCount];
    //    trailRenderer.GetPositions(positions);

    //    for (var i = 0; i < positions.Length; i++)
    //    {
    //        positions[i] += offset;
    //    }

    //    trailRenderer.SetPositions(positions);
    //}
}
