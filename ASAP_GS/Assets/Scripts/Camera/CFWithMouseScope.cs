using UnityEngine;

public class CFWithMouseScope : CameraFollow
{
    //ToDo: add in subclass
    [SerializeField] private float _scopingScale = 10f;

    protected override Vector3 GetLerpPosition(Transform followTarget, float zAxisOffset)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        Vector3 target = GetTargetpPosition(followTarget, zAxisOffset);
        Vector2 mouseScopeNormal = (mousePosition - followTarget.position).normalized;

        Vector3 mouseScopeVector = new Vector3()
        {
            x = target.x + mouseScopeNormal.x * _scopingScale,
            y = target.y + mouseScopeNormal.y * _scopingScale,
            z = zAxisOffset
        };

        Vector3 lerpPosition = Vector3.Lerp(transform.position, mouseScopeVector, _speed * Time.deltaTime);
        return lerpPosition;
    }
}
