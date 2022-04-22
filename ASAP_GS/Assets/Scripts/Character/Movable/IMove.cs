using UnityEngine;

public interface IMove
{
    void Run(Vector2 normalizeVelocity);
    void Dash(Vector2 normalizeVelocity);
}
