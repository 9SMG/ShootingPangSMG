using UnityEngine;

/*
 * 화면을 좌클릭했을 때, 발사 준비
 * 발사준비 상태(좌클릭)에서 드레그하면 방향과 힘 조절 가능
 * 발사준비 상태(좌클릭)에서 좌클릭을 때면, 발사
 * 발사준비 상태(좌클릭)에서 우클릭을 하면, 취소
 */



public class SMGPlayerController : MonoBehaviour
{
    public SMGBullet bullet;
    public delegate void HitBullet2(Vector2 hit);
    public HitBullet2 HitBullet3;

    Gage gage;    
    Vector2 mouseButtonDownPos;

    bool isDragable = false;
    float hitVectorScale = 11f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gage = GetComponentInChildren<Gage>();

        //Vector2 test1 = new Vector2(1, 1);
        //Vector2 test2 = new Vector2(1, 1);
        //Debug.Log("test1: " + test1);
        //test1 *= 2f;
        //Debug.Log("test1: " + test1);

        //Debug.Log("test2: " + test2);
        //test2 = test2.normalized * 2f;
        //Debug.Log("test2: " + test2);


        //Vector2 test3 = new Vector2(1, 0);
        //Vector2 test4 = new Vector2(1, 1);
        //Debug.Log("angle: " + Vector2.Angle(test3, test4));
        //Debug.Log("angle: " + Mathf.Atan2(test4.y, test4.x) * Mathf.Rad2Deg);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isDragable = true;
            mouseButtonDownPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isDragable = false;

            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dragVector = (Vector2)transform.position - currentMousePosition;
            Vector2 hitVector = dragVector.normalized * gage.GetGage() * hitVectorScale;
            
            HitBullet(hitVector);
        }

        if(Input.GetMouseButtonDown(1))
        {
            isDragable = false;
        }

        DragGage(isDragable);
        //if (isDragable)
        //{
        //    Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    //Vector2 dragVector = (Vector2)transform.position - currentMousePosition;
        //    Vector2 dragVector = mouseButtonDownPos - currentMousePosition;

        //    // gage rotate
        //    float angle = Mathf.Atan2(dragVector.y, dragVector.x) * Mathf.Rad2Deg;
        //    transform.eulerAngles = new Vector3(0, 0, angle);

        //    // gage power
        //    gage.SetGage(dragVector.magnitude);
        //}
        //else
        //{
        //    gage.SetGage(0f);
        //}
    }

    void DragGage(bool isDragable)
    {
        if (isDragable)
        {
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector2 dragVector = (Vector2)transform.position - currentMousePosition;
            Vector2 dragVector = mouseButtonDownPos - currentMousePosition;

            // gage rotate
            float angle = Mathf.Atan2(dragVector.y, dragVector.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);

            // gage power
            gage.SetGage(dragVector.magnitude);
        }
        else
        {
            gage.SetGage(0f);
        }
    }

    void HitBullet(Vector2 hit)
    {
        bullet.HitBall(hit);
    }
}
