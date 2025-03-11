using UnityEngine;

public class StageSettings : MonoBehaviour
{
    //[SerializeField] private int itemBomb;      // 폭탄: 일정 범위 내 벽 파괴
    //[SerializeField] private int itemMagnet;    // 자석: 일정 범위 내 아이템 당기기
    //[SerializeField] private int itemKnockBack; // 밀치기: 일정 범위 내 벽 밀치기
    //[SerializeField] private int itemImotal;    // 무적: 특정 기믹 회피
    //[SerializeField] private int itemCleaner;   // 세정제: 특정 기믹 회피
    //[SerializeField] private int itemZeroGravity;   // 무중력: 특정 기믹 회피

    [SerializeField] private int[] items = new int[4];

    [SerializeField] private int numCoin;
    [SerializeField] private int numBullet;

    [SerializeField] private Vector3 playerPosition;

    [SerializeField] private int cameraSize;


    private void Awake()
    {
        //SetStage();
    }

    private void SetStage()
    {
        // Item은 ItemContoller
        // Coin은 GameManager
        // Bullet은 PlayerController
        // 이 무슨 중구난방 구조인가
        //ItemController.Instance.items[(int)Item.Bomb] = itemBomb;
        //ItemController.Instance.items[(int)Item.Magnet] = itemMagnet;
        //ItemController.Instance.items[(int)Item.KnockBack] = itemKnockBack;
        //ItemController.Instance.items[(int)Item.Imotal] = itemImotal;
        //ItemController.Instance.items[(int)Item.Cleaner] = itemCleaner;
        //ItemController.Instance.items[(int)Item.ZeroGravity] = itemZeroGravity;
        ItemController.Instance.InitItem(items);

        //GameManager.Instance.remainCoin = numCoin;
        GameManager.Instance.InitCoin(numCoin);

        //PlayerController.Instance.bulletPossess = numBullet;
        PlayerController.Instance.InitBullet(numBullet);

        //PlayerController.Instance.gameObject.transform.position = PlayerPosition;
        PlayerController.Instance.InitPosition(playerPosition);

        Camera.main.orthographicSize = cameraSize;
    }

    public int GetItemCount(int item)
    {
        return items[item];
    }
    public int GetCoinNum()
    {
        return numCoin;
    }
    
    public int GetBulletNum()
    {
        return numBullet;
    }

    public Vector3 GetStartPos()
    {
        return playerPosition;
    }

    public int GetCameraSize()
    {
        return cameraSize;
    }
}
