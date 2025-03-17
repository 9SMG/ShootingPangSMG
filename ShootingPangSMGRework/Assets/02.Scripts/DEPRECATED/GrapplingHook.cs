using System.Reflection;
using UnityEngine;

public class GrapplingHook : BallAbility
{
    LineRenderer lineRenderer;
    DistanceJoint2D distJoint;

    Rigidbody2D parentRb;
    Rigidbody2D rb;

    RaycastHit2D hit;
    Vector3 mousePos;
    //Vector2 localHitPoint;
    Vector2 anchorPos, localAnchorPos;

    int rayLayerMask;
    float orginDamping;

    Collider2D col;
    Color aimColor = new Color(0f, 0.9f, 1f);
    Color chainColor = new Color(0.45f, 0.45f, 0.45f);

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        distJoint = GetComponent<DistanceJoint2D>();
        parentRb = transform.parent.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        rayLayerMask = 1 << LayerMask.NameToLayer("Wall");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        distJoint.enabled = false;
    }


    bool isSwing;
    void Update()
    {
        if (GameManager.Instance.GetDragable())
        {
            if (distJoint.enabled)
                distJoint.enabled = false;
            lineRenderer.SetPosition(1, Vector3.zero);

            return;
        }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dir = mousePos - transform.position;

        //hit = Physics2D.Raycast(transform.position, dir.normalized, 50f, rayLayerMask);
        //localAnchorPos = transform.InverseTransformPoint(anchorPos);

        UpdateLineRenderer();

        if (Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.Raycast(transform.position, dir.normalized, 50f, rayLayerMask);
            if(hit && !hit.transform.CompareTag(TagManager.tagGrapplingAble))
            {
                isSwing = true;

                anchorPos = hit.point;
                localAnchorPos = transform.InverseTransformPoint(anchorPos);

                // Swing
                distJoint.enabled = true;
                distJoint.connectedAnchor = anchorPos;
                distJoint.distance = Vector2.Distance(anchorPos, transform.position);

                rb.linearVelocity = parentRb.linearVelocity;
                parentRb.linearVelocity = Vector2.zero;

                rb.mass = parentRb.mass;
                parentRb.mass = 0f;

                //col.enabled = true;

                Vector2 tangent_a = new Vector2(localAnchorPos.y, -localAnchorPos.x);
                Vector2 tangent_b = new Vector2(-localAnchorPos.y, localAnchorPos.x);
                if (Vector2.Angle(tangent_a, rb.linearVelocity) < Vector2.Angle(tangent_b, rb.linearVelocity))
                {
                    rb.linearVelocity = tangent_a.normalized * rb.linearVelocity.magnitude;
                }
                else
                {
                    rb.linearVelocity = tangent_b.normalized * rb.linearVelocity.magnitude;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if(isSwing)
            {
                lineRenderer.SetPosition(1, Vector3.zero);

                distJoint.enabled = false;

                //parentRb.linearDamping = orginDamping;

                parentRb.linearVelocity = rb.linearVelocity;
                rb.linearVelocity = Vector2.zero;

                parentRb.mass = rb.mass;
                rb.mass = 0f;

                //col.enabled = false;

                isSwing = false;
                isSwingDashed = false;
            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            if(isSwing && !isSwingDashed)
            {
                isSwingDashed = true;
                rb.linearVelocity = rb.linearVelocity.normalized * 15f;
            }
        }
    }
    bool isSwingDashed = false;

    void UpdateLineRenderer()
    {
        if (Input.GetMouseButton(0))
        {
            if(isSwing)
            {
                lineRenderer.SetPosition(1, transform.InverseTransformPoint(anchorPos));

                // Color
                {
                    Color fullAlphaColor = chainColor;
                    fullAlphaColor.a = 1f;
                    lineRenderer.startColor = fullAlphaColor;

                    fullAlphaColor = chainColor;
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

    public override void StopAbility()
    {
        //throw new System.NotImplementedException();
    }
}
