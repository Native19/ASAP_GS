using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWithPlayer : CameraFollow
{
    protected override Vector3 GetLerpPosition(Transform followTarget, float zAxisOffset)
    {
        Vector3 targetPosition = new Vector3(followTarget.position.x, followTarget.position.y, zAxisOffset);
        return targetPosition;
    }
}
