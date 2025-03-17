using UnityEngine;

public class BallBag : MonoBehaviour
{
    BallUI[] balls;
    int currIdx;

    private void Awake()
    {
        balls = GetComponentsInChildren<BallUI>();
    }
    private void Start()
    {
        //currIdx = 0;
        ResetEnableBall(10);
    }

    public void ResetEnableBall(int count)
    {
        currIdx = 0;
        for (int i = 0; i <balls.Length; i++)
        {
            balls[i].SetUsed(false);
            balls[i].SetSelect(false);
            balls[i].ShowSelectItem(-1);//Item.NoItem);
            balls[i].gameObject.SetActive(i < count ? true : false);
        }
        balls[0].SetSelect(true);
    }

    public void SetCurrBallItem(int item)//Item item)
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
