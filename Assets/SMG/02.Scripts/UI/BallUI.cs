using UnityEngine;
using UnityEngine.UI;

public class BallUI : MonoBehaviour
{
    GameObject objUsed;
    Image imgSelect;

    void Start()
    {
        imgSelect = GetComponent<Image>();
        objUsed = transform.GetChild(transform.childCount - 1).gameObject;
    }

    #region ContextMenu Function
    [ContextMenu("SetUsed(true)")]
    public void TestSetUsed()
    {
        SetUsed(true);
    }

    [ContextMenu("SetUsed(false)")]
    public void TestSetunUse()
    {
        SetUsed(false);
    }

    [ContextMenu("SetSelect(true)")]
    public void TestSetSelectOn()
    {
        SetSelect(true);
    }

    [ContextMenu("SetSelect(false)")]
    public void TestSetSelectOff()
    {
        SetSelect(false);
    }


    #endregion

    // visible

    // 선택 여부
    public void SetSelect(bool select)
    {
        Color currentColor = imgSelect.color;
        currentColor.a = (select ? 200f : 0f) / 255f;
        imgSelect.color = currentColor;
    }

    // 사용 여부
    public void SetUsed(bool used)
    {
        objUsed.SetActive(used);
    }

    // 선택된 아이템
    public void ShowSelectItem(Item item)
    {
        //Item
    }
    // 
}
