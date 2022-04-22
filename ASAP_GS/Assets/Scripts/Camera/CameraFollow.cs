using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;
    [SerializeField] private string _playerTag = "Player";
    [SerializeField] protected float _speed = 20f;
    [SerializeField] private float _zAxisOffset = -10f;

    protected void Awake()
    {
        if (_followTarget)
            return;
        _followTarget = GameObject.FindGameObjectWithTag(_playerTag).transform;

        if (_followTarget)
        {
            transform.position = GetTargetpPosition(_followTarget, _zAxisOffset);
        }

    }

    protected void Update()
    {
        if (!_followTarget)
            return;

        transform.position = GetLerpPosition(_followTarget, _zAxisOffset);
    }

    protected virtual Vector3 GetLerpPosition (Transform followTarget, float zAxisOffset)
    {
        Vector3 target = GetTargetpPosition(followTarget, zAxisOffset);
        Vector3 lerpPosition = Vector3
            //.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            .Lerp(transform.position, target, _speed * Time.deltaTime);

        return lerpPosition;
    }

    protected Vector3 GetTargetpPosition(Transform followTarget, float zAxisOffset)
    {
        return new Vector3()
        {
            x = followTarget.position.x,
            y = followTarget.position.y,
            z = followTarget.position.z + zAxisOffset
        };
    }
}
