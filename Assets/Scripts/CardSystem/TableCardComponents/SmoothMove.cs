using UnityEngine;

public class SmoothMove : MonoBehaviour
{
    public float _speed = 0.2f;
    private Vector3 _targetPosition;
    private Vector3 _correntVelocity;
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
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition, _targetPosition, ref _correntVelocity, _speed);
            }
            else 
            {
                StopMove();
            }
        }
    }
}
