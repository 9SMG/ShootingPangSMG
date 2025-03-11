using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/*
 * 화면을 좌클릭했을 때, 발사 준비
 * 발사준비 상태(좌클릭)에서 드레그하면 방향과 힘 조절 가능
 * 발사준비 상태(좌클릭)에서 좌클릭을 때면, 발사
 * 발사준비 상태(좌클릭)에서 우클릭을 하면, 취소
 */



public class SMGPlayerController : MonoBehaviour
{
    public static SMGPlayerController Instance;

    Gage gage;    
    Vector2 mouseButtonDownPos;
    
    float hitVectorScale = 10f;
    bool isDraging = false;
    bool mouseHoverUI;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        gage = GetComponentInChildren<Gage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!SMGGameManager.Instance.GetDragable())
        {
            if (isDraging)
            {
                isDraging = false;
                DragGage(isDraging);
            }
            return;
        }
        mouseHoverUI = EventSystem.current.IsPointerOverGameObject();

        if (Input.GetMouseButtonDown(0) && !isDraging && !mouseHoverUI)
        {
            isDraging = true;
            mouseButtonDownPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0) && isDraging)
        {
            isDraging = false;
            HitBullet();
        }

        if (Input.GetMouseButtonDown(1))
        {
            isDraging = false;
        }
        

        DragGage(isDraging);
    }

    void DragGage(bool isDraging)
    {
        if (isDraging)
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

    void HitBullet()
    {
        Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 dragVector = (Vector2)transform.position - currentMousePosition;
        Vector2 dragVector = mouseButtonDownPos - currentMousePosition;
        // 0 Vector Cancel
        if (dragVector == Vector2.zero)
            return;
        Vector2 hitVector = dragVector.normalized * gage.GetGage() * hitVectorScale;

        //bullet.HitBall(hit);
        SMGGameManager.Instance.HitBullet(hitVector);
    }
}
