using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TrailVisible : MonoBehaviour
{
    TrailRenderer trail;

    float trailOldTime = 0f;    // SetVisibleTrail() 외 변경 금지
    bool hasBeenSet = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trail = GetComponent<TrailRenderer>();
    }

    [ContextMenu("SetVisible(true)")]
    void TestTrailOn()
    {
        SetVisible(true);
    }

    [ContextMenu("SetVisible(false)")]
    void TestTrailOff()
    {
        SetVisible(false);
    }

    [ContextMenu("SetVisibleTimer(true, 10f)")]
    void TestVisibleTimer()
    {
        //SetVisible(false);
        SetVisibleTimer(true, 10f);
    }

    public void SetVisible(bool visible)
    {
        hasBeenSet = true;
        //GetComponent<TrailRenderer>().enabled = visible;

        if (visible)
        {
            if (trailOldTime > 0)
                trail.time = trailOldTime;
        }
        else
        {
            if (trail.time > 0)
                trailOldTime = trail.time;
            trail.time = 0f;
        }

    }

    public void SetVisibleTimer(bool visible, float delayTime, bool hasNext = false)
    {
        //Invoke("SetVisible(" + visible.ToString() + ")", delayTime);
        hasBeenSet = false;
        StartCoroutine(VisibleTimer(visible, delayTime, hasNext));
    }

    IEnumerator VisibleTimer(bool visible, float delayTime, bool hasNext)
    {
        yield return new WaitForSeconds(delayTime);

        if (hasBeenSet)
            yield break;
        SetVisible(visible);
        hasBeenSet = !hasNext;
    }
}
