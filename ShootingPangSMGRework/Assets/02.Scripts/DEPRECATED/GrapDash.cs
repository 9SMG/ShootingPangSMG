using UnityEngine;

public class GrapDash : BallAbility
{
    LineRenderer lineRenderer;
    
    Rigidbody2D parentRb;
    Rigidbody2D rb;

    RaycastHit2D hit;
    Vector3 mousePos;
    //Vector2 localHitPoint;
    //Vector2 anchorPos, localAnchorPos;

    int rayLayerMask;
    
    Collider2D col;
    Color aimColor = new Color(0f, 0.9f, 1f);
    

    Collider2D grapCol;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        parentRb = transform.parent.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        rayLayerMask = 1 << LayerMask.NameToLayer("Wall");
    }

    bool isGrapping;

    void Update()
    {
        if (GameManager.Instance.GetDragable())
        {
            lineRenderer.SetPosition(1, Vector3.zero);

            return;
        }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector2 dir = mousePos - transform.position;

        //hit = Physics2D.Raycast(transform.position, dir.normalized, 50f, rayLayerMask);
        //if(hit.transform.CompareTag(TagManager.tagGrapplingAble))
        //{
        //    //lineRenderer.enabled = true;
        //    localAnchorPos = transform.InverseTransformPoint(anchorPos);
        //}
        //else
        //{
        //    //lineRenderer.enabled = false;
        //}

        UpdateLineRenderer();
        if (Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.Raycast(transform.position, dir.normalized, 50f, rayLayerMask);
            if(hit && hit.transform.CompareTag(TagManager.tagGrapplingAble))
            {
                isGrapping = true;

                grapCol = hit.collider;
                //grapCol.enabled = false;
                
                transform.parent.position = hit.transform.position;
                parentRb.linearVelocity = Vector2.zero;
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(isGrapping)
            {
                transform.parent.GetComponent<BallController>().HitBall(dir.normalized * 15f);
                //parentRb.linearVelocity = dir.normalized * 40f;

                //grapCol.enabled = true;
                isGrapping = false;
            }
        }
    }


    void UpdateLineRenderer()
    {
        if (Input.GetMouseButton(0))
        {
            if (isGrapping)
            {
                Vector2 dir = mousePos - transform.position;
                lineRenderer.SetPosition(1, dir.normalized);// transform.InverseTransformPoint(transform.position + ()));

                // Color
                {
                    Color fullAlphaColor = aimColor;
                    fullAlphaColor.a = 1f;
                    lineRenderer.startColor = fullAlphaColor;

                    fullAlphaColor = aimColor;
                    fullAlphaColor.a = 1f;
                    lineRenderer.endColor = fullAlphaColor;
                }
            }
        }
        //else
        //{
        //    lineRenderer.SetPosition(1, transform.InverseTransformPoint(hit.point));

        //    Color semiAlphaColor = aimColor;
        //    semiAlphaColor.a = 0.2f;
        //    lineRenderer.startColor = semiAlphaColor;

        //    semiAlphaColor = aimColor;
        //    semiAlphaColor.a = 0.2f;
        //    lineRenderer.endColor = semiAlphaColor;
        //}

    }
}
