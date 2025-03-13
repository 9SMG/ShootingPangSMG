using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TrailVisible : MonoBehaviour
{
    TrailRenderer trail;

    float trailOldTime = 0f;    // SetVisibleTrail() 외 변경 금지
    bool hasBeenSet = false;
    Coroutine lastCoroutine;
    
    void Start()
    {
        trail = GetComponent<TrailRenderer>();
    }

    public void SetVisible(bool visible)
    {
        hasBeenSet = true;
        
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

    public void SetVisibleTimer(bool visible, float delayTime, bool cancelPrevTimer = true)
    {
        if(lastCoroutine != null && cancelPrevTimer)
        {
            StopCoroutine(lastCoroutine);
        }
        hasBeenSet = false;
        lastCoroutine = StartCoroutine(VisibleTimer(visible, delayTime));
    }

    public void AllVisibleTimerClear()
    {
        StopAllCoroutines();
    }

    IEnumerator VisibleTimer(bool visible, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        if (hasBeenSet)
            yield break;
        SetVisible(visible);
        hasBeenSet = false;
        lastCoroutine = null;
    }

    #region ContextMenu Func
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
        SetVisibleTimer(true, 10f);
    }
    #endregion
}
