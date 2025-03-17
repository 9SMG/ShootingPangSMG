using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    BallController myBall;
    Gage myBallGage;

    Vector2 mouseButtonDownPos;
    Vector2 currentMousePosition;
    Vector2 dragVector;

    float hitVectorScale = 10f;
    bool isDraging = false;
    bool mouseHoverUI;

    private void Awake()
    {
        myBall = FindFirstObjectByType<BallController>();
        myBallGage = myBall.GetComponentInChildren<Gage>();
    }

    void Update()
    {
        if (!GameManager.Instance.GetDragable())
        {
            if (isDraging)
            {
                isDraging = false;
                DragGage(isDraging);
            }
            return;
        }
        mouseHoverUI = EventSystem.current.IsPointerOverGameObject();
        //mouseHoverUI = false;

        if ((Input.GetMouseButtonDown(0)|| GameManager.Instance.GetAgainDrag()) && !isDraging && !mouseHoverUI)
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
            currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragVector = mouseButtonDownPos - currentMousePosition;

            // gage rotate
            float angle = Mathf.Atan2(dragVector.y, dragVector.x) * Mathf.Rad2Deg;
            myBallGage.transform.eulerAngles = new Vector3(0, 0, angle);

            // gage power
            myBallGage.SetGage(dragVector.magnitude);
        }
        else
        {
            myBallGage.SetGage(0f);
        }
    }

    void HitBullet()
    {
        //Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 dragVector = mouseButtonDownPos - currentMousePosition;
        
        // 0 Vector Cancel
        if (dragVector == Vector2.zero)
            return;
        
        Vector2 hitVector = dragVector.normalized * myBallGage.GetGage() * hitVectorScale;
        //myBall.HitBall(hitVector);
        GameManager.Instance.HitBall(hitVector);
    }
}
