using UnityEngine;

public class SmoothFoolowParent : MonoBehaviour
{
    [SerializeField] private float step = .05f;

    private Vector3 startLocalPos, lastFramePos, lastDesiredPos, fromPos;
    private Quaternion startLocalRot, lastFrameRot, lastDesiredRot, fromRot;

    private float percent;
    private float constZ_Pos;

    private bool _isFollow;
    public void StartFollow() 
    {
        _isFollow = true;
        startLocalPos = transform.localPosition;
        startLocalRot = transform.localRotation;

        lastFramePos = transform.position;
        lastFrameRot = transform.rotation;

        constZ_Pos = startLocalPos.z;
    }

    public void StopFollow() 
    { 
        _isFollow = false;
    }

    void Update()
    {
        if (_isFollow) 
        { 
            Vector3 newDesiredPos = transform.parent.TransformPoint(startLocalPos);
            Quaternion newDesiredRot = transform.parent.rotation * startLocalRot;

            if (lastDesiredPos != newDesiredPos || lastDesiredRot != newDesiredRot)
            {
                percent = 0;

                lastDesiredPos = newDesiredPos;
                lastDesiredRot = newDesiredRot;

                fromPos = lastFramePos;
                fromRot = lastFrameRot;
            }

            if (percent <= 1)
            {
                percent += step;

                lastFramePos = Vector3.Lerp(fromPos, newDesiredPos, percent);
                lastFrameRot = Quaternion.Lerp(fromRot, newDesiredRot, percent);

                Vector3 newLocalPos = transform.parent.TransformPoint(lastFramePos);
                newLocalPos.z = constZ_Pos;

                transform.localPosition = newLocalPos;
                transform.rotation = lastFrameRot;
            }
        }

    }
}
