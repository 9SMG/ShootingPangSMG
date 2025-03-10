using UnityEngine;
using static UnityEditor.Progress;

public class BallBag : MonoBehaviour
{
    BallUI[] balls;
    int currIdx;

    private void Start()
    {
        balls = GetComponentsInChildren<BallUI>();
        currIdx = 0;
        ResetEnableBall(10);
    }

    public void ResetEnableBall(int count)
    {
        for (int i = 0; i <balls.Length; i++)
        {
            balls[i].SetUsed(false);
            balls[i].SetSelect(false);
            balls[currIdx].ShowSelectItem(Item.NoItem);
            balls[i].gameObject.SetActive(i < count ? true : false);
        }
        balls[0].SetSelect(false);
    }

    public void SetCurrBallItem(Item item)
    {
        balls[currIdx].ShowSelectItem(item);
    }

    public void SetNextBall()
    {
        balls[currIdx].SetSelect(false);
        balls[currIdx++].SetUsed(true);
        balls[currIdx].SetSelect(true);
    }
}
