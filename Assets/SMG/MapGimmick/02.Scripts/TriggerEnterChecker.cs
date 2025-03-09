using UnityEngine;

public class TriggerEnterChecker : MonoBehaviour
{
    public LayerMask targetLayer = -1;  // default Everything
    public string[] targetTags = { "Bullet" };

    public bool isEnter = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Layer Filter
        if ((targetLayer.value & 1 << collision.gameObject.layer) == 0)
            return;

        // Tag Fillter
        bool hasTagElements = targetTags.Length > 0 ? true : false;
        if (hasTagElements)
        {
            bool isTargetTag = false;
            foreach (string _tag in targetTags)
            {
                if (collision.CompareTag(_tag) == true)
                {
                    isTargetTag = true;
                    break;
                }
            }
            if (!isTargetTag)
                return;
        }
        isEnter = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //// Layer Filter
        if ((targetLayer.value & 1 << collision.gameObject.layer) == 0)
            return;

        // Tag Fillter
        bool hasTagElements = targetTags.Length > 0 ? true : false;
        if (hasTagElements)
        {
            bool isTargetTag = false;
            foreach (string _tag in targetTags)
            {
                if (collision.CompareTag(_tag) == true)
                {
                    isTargetTag = true;
                    break;
                }
            }
            if (!isTargetTag)
                return;
        }
        isEnter = false;
    }
}
