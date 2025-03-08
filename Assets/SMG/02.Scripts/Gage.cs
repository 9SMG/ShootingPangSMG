using UnityEngine;

public class Gage : MonoBehaviour
{
    Transform spriteMaskParent;
    float maxGage = 1.5f;

    void Start()
    {
        spriteMaskParent = GetComponentInChildren<SpriteMask>().transform.parent;
    }

    public void SetGage(float gage)
    {
        //Vector3 beforeScale = spriteMaskParent.transform.localScale;
        //spriteMaskParent.transform.localScale = new Vector3(gage, beforeScale.y, beforeScale.z);
        gage = Mathf.Clamp(gage, 0, maxGage);
        spriteMaskParent.transform.localScale = new Vector3(gage, 1f, 1f);
    }

    public float GetGage()
    {
        return spriteMaskParent.transform.localScale.x;
    }
}
