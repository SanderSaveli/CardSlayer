using System.Collections;
using UnityEngine;

public class SmoothFoolowParent : MonoBehaviour
{
    [SerializeField] private float step = 0.2f;

    private Vector3 startLocalPos, lastFramePos, lastDesiredPos, fromPos;
    private Quaternion startLocalRot, lastFrameRot, lastDesiredRot, fromRot;

    private float percent;
    private float constZ_Pos;

    private bool _isAcktive;
    private bool _isRunning;
    public void StartFollow() 
    {
        startLocalPos = transform.localPosition;
        startLocalRot = transform.localRotation;

        lastFramePos = transform.position;
        lastFrameRot = transform.rotation;

        constZ_Pos = startLocalPos.z;
        _isAcktive = true;
        if (!_isRunning) 
        {
            _isRunning = true;
            StartCoroutine(FollowParent());
        }
    }

    public void StopFollow() 
    { 
        _isAcktive = false;
    }

    private IEnumerator FollowParent() 
    {
        while (_isAcktive || lastDesiredPos == transform.parent.TransformPoint(startLocalPos)) 
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

                Vector3 newLocalPos = transform.parent.InverseTransformPoint(lastFramePos);
                newLocalPos.z = constZ_Pos;

                transform.localPosition = newLocalPos;
                transform.rotation = lastFrameRot;
            }
            yield return null;
        }
        _isRunning = false;
    }
}
