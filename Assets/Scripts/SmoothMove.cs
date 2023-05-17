using UnityEngine;

public class SmoothMove : MonoBehaviour
{
    private Vector3 _targetPosition;
    private bool _isMoving = false;

    private SmoothFoolowParent followParent;

    private void Awake()
    {
        followParent = GetComponent<SmoothFoolowParent>();
    }
    public void MoveTo(Vector3 Position)
    {
        _targetPosition = Position;
        _isMoving = true;
        followParent.StopFollow();
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
                transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPosition, 6 * Time.deltaTime);
            }
            else 
            {
                StopMove();
                followParent.StartFollow();
            }
        }
    }
}
