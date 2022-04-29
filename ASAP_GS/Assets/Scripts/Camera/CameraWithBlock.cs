using UnityEngine;

public class CameraWithBlock : CameraWithPlayer
{
    [SerializeField] Vector2 leftUpBlock;
    [SerializeField] Vector2 RightDownBlock;
    protected override Vector3 GetLerpPosition(Transform followTarget, float zAxisOffset)
    {
        Vector3 targetPosition = new Vector3(
            Mathf.Clamp(followTarget.position.x, leftUpBlock.x, RightDownBlock.x),
            Mathf.Clamp(followTarget.position.y, RightDownBlock.y, leftUpBlock.y),
            zAxisOffset);
        return targetPosition;
    }
}
