using UnityEngine;

public class SmoothMove : MonoBehaviour
{
    public int _speed = 6;
    private Vector3 _targetPosition;
    private bool _isMoving = false;
    public void MoveTo(Vector3 Position)
    {
        _targetPosition = Position;
        _isMoving = true;
    }

    public void StopMove() 
    {
        _isMoving = false;
    }

    private void FixedUpdate()
    {
        if (_isMoving) 
        {
            if(transform.localPosition != _targetPosition) 
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPosition, _speed * Time.deltaTime);
            }
            else 
            {
                StopMove();
            }
        }
    }
}
