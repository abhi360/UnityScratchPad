using UnityEngine;
using System.Collections;

public class SplineWalker : MonoBehaviour
{
    public bool lookForward;
    public BezierSpline spline;

    public float duration;

    private float progress;

    public float speed;

    private void Update()
    {
        progress += Time.deltaTime / duration * speed;
        if (progress > 1f)
        {
            progress = 1f;
        }
        Vector3 position = spline.GetPoint(progress);
        transform.localPosition = position;
        if (lookForward)
        {
            transform.LookAt(position + spline.GetDirection(progress));
        }
    }
}
