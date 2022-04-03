using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;
    [SerializeField] private string _playerTag = "Player";
    [SerializeField] protected float _speed = 5f;
    [SerializeField] private float _zAxisOffset = -10f;
    //private bool _targetIsMooving = false;

    protected void Awake()
    {
        if (_followTarget)
            return;
        _followTarget = GameObject.FindGameObjectWithTag(_playerTag).transform;

        if (_followTarget)
        {
            transform.position = GetTargetpPosition(_followTarget, _zAxisOffset);
            //_targetIsMooving = _followTarget.GetComponent<Move>().IsMooving();
        }

    }

    protected void Update()
    {
        if (!_followTarget)
            return;

        //if (_followTarget.GetComponent<Move>().IsMooving())
        //    transform.position = GetTargetpPosition(_followTarget, _zAxisOffset);
        //else
        //    transform.position = GetLerpPosition(_followTarget, _zAxisOffset);

        transform.position = GetLerpPosition(_followTarget, _zAxisOffset);
    }

    protected virtual Vector3 GetLerpPosition (Transform followTarget, float zAxisOffset)
    {
        Vector3 target = GetTargetpPosition(followTarget, zAxisOffset);
        Vector3 lerpPosition = Vector3.Lerp(transform.position, target, _speed * Time.deltaTime);

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
