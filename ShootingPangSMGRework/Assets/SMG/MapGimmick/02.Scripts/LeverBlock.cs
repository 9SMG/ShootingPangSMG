using UnityEngine;

public class LeverBlock : MonoBehaviour
{
    public LayerMask targetLayer = -1;  // default Everything
    public string[] targetTags = { "Bullet" };
    
    public bool isOn = false;

    public AudioClip sfx;

    //public Vector2 On_minXY = new Vector2(-1,-1);
    //public Vector2 On_maxXY = new(0, 1);

    //public Vector2 Off_minXY = new Vector2(0,-1);
    //public Vector2 Off_maxXY = new(1, 1);

    //public SpriteRenderer aButton;
    public TriggerEnterChecker onTrigger;
    public TriggerEnterChecker offTrigger;

    SpriteRenderer onSpriteRenderer;
    SpriteRenderer offSpriteRenderer;


    Color32 onColor = new Color32(0, 0, 255, 255);
    Color32 offColor = new Color32(255, 0, 0, 255);
    Color onDimColor;
    Color offDimColor;

    private void Start()
    {
        onSpriteRenderer = onTrigger.GetComponent<SpriteRenderer>();
        offSpriteRenderer = offTrigger.GetComponent<SpriteRenderer>();

        onDimColor = GetDimColor(onColor);
        offDimColor = GetDimColor(offColor);

        onSpriteRenderer.color = isOn ? onColor : onDimColor;
        offSpriteRenderer.color = isOn ? offDimColor : offColor;
    }

    Color GetDimColor(Color color)
    {
        float h, s, v;
        Color.RGBToHSV(color, out h, out s, out v);
        return Color.HSVToRGB(h, 0.45f, 0.45f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
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
                if (collision.gameObject.CompareTag(_tag) == true)
                {
                    isTargetTag = true;
                    break;
                }
            }
            if (!isTargetTag)
                return;
        }

        //if (onlyBullet && !collision.gameObject.CompareTag(tagBullet))
        //    return;

        if (onTrigger.isEnter)
            isOn = true;
        else if (offTrigger.isEnter)
            isOn = false;

        onSpriteRenderer.color = isOn ? onColor : onDimColor;
        offSpriteRenderer.color = isOn ? offDimColor : offColor;

#if false
#region 추가 콜라이더 사용 안한 버전, (회전했을때 사용 못함)

        //Debug.Log("collision.transform.position: " + collision.transform.position);
        //Debug.Log("collision.GetContact(0).point: " + transform.InverseTransformPoint(collision.GetContact(0).point));

        Rect onArea = new Rect(On_minXY.x, On_minXY.y, 
            On_maxXY.x - On_minXY.x, 
            On_maxXY.y - On_minXY.y);

        Rect offArea = new Rect(Off_minXY.x, Off_minXY.y,
            Off_maxXY.x - Off_minXY.x,
            Off_maxXY.y - Off_minXY.y);

        //Debug.Log(onArea.Contains(transform.InverseTransformPoint(collision.GetContact(0).point)));

        // On
        if(onArea.Contains(transform.InverseTransformPoint(collision.GetContact(0).point)))
        {
            if(!isOn)
                SoundsPlayer.Instance.PlaySFX(sfx);
            isOn = true;
        }
        else if (offArea.Contains(transform.InverseTransformPoint(collision.GetContact(0).point)))
        {
            if(isOn)
                SoundsPlayer.Instance.PlaySFX(sfx);
            isOn = false;
        }
#endregion
#endif

    }
#if false
#region Point가 회전한 Rect 영역 내 포함인지 확인하는 코드 Chat-GPT(미완)
    public Vector2 rectCenter = new Vector2(0, 0);
    public Vector2 rectSize = new Vector2(5, 3);
    public float rotationAngle = 45f; // Rotation in degrees


    private void Start()
    {
        for(int i = -10; i < 10; i++)
        {
            for(int j = -10; j < 10; j++)
            {
                Vector2 test = new Vector2(i, j);
                Debug.Log("Vector: " + test + ", In Range: " + IsPointInsideRotatedRect(test));
            }
        }

        Bounds what = aButton.bounds;
        Debug.Log("what.size: " + what.size + " ,what.max: " + what.max + " ,what.min: " + what.min + " ,what.center: " + what.center);
        
        Rect testArea = new Rect(what.center, what.size);
        Debug.Log(testArea);
    }

    void Update()
    {
        Vector2 point = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Debug.Log("Mouse Point: " + point);


        if (IsPointInsideRotatedRect(point))
        {
            Debug.Log("Point is inside the rotated rect.");
        }
        else
        {
            Debug.Log("Point is outside the rotated rect.");
        }
    }

    bool IsPointInsideRotatedRect(Vector2 point)
    {
        // Get the four corners of the rectangle
        Vector2[] corners = GetRotatedRectCorners(rectCenter, rectSize, rotationAngle);

        // Check if the point is inside the rotated rectangle using a point-in-polygon test
        return PointInPolygon(corners, point);
    }

    Vector2[] GetRotatedRectCorners(Vector2 center, Vector2 size, float angle)
    {
        Vector2[] corners = new Vector2[4];
        Vector2 halfSize = size / 2;

        // Define the rectangle corners (unrotated)
        corners[0] = center + new Vector2(-halfSize.x, -halfSize.y);
        corners[1] = center + new Vector2(halfSize.x, -halfSize.y);
        corners[2] = center + new Vector2(halfSize.x, halfSize.y);
        corners[3] = center + new Vector2(-halfSize.x, halfSize.y);

        // Apply rotation to each corner
        for (int i = 0; i < corners.Length; i++)
        {
            corners[i] = RotatePoint(corners[i], center, angle);
        }

        return corners;
    }

    Vector2 RotatePoint(Vector2 point, Vector2 pivot, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        // Translate point to origin, rotate, and then translate back
        float x = cos * (point.x - pivot.x) - sin * (point.y - pivot.y) + pivot.x;
        float y = sin * (point.x - pivot.x) + cos * (point.y - pivot.y) + pivot.y;

        return new Vector2(x, y);
    }

    bool PointInPolygon(Vector2[] polygon, Vector2 point)
    {
        bool inside = false;
        int n = polygon.Length;
        for (int i = 0, j = n - 1; i < n; j = i++)
        {
            if (((polygon[i].y > point.y) != (polygon[j].y > point.y)) &&
                (point.x < (polygon[j].x - polygon[i].x) * (point.y - polygon[i].y) / (polygon[j].y - polygon[i].y) + polygon[i].x))
            {
                inside = !inside;
            }
        }
        return inside;
    }
#endregion
#endif
}
