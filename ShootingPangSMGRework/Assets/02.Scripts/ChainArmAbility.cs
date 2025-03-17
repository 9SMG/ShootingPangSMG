#define USE_GAMEMANAGER

using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChainArmAbility : BallAbility
{
    GrapableBlock grapableBlock;

    LineRenderer lineRenderer;
    DistanceJoint2D distJoint;

    Rigidbody2D parentRb;
    Rigidbody2D rb;

    RaycastHit2D hit;

    Vector3 mousePos;
    Vector2 mouseDir;
    Vector2 anchorPos, localAnchorPos;

    Color aimColor = new Color(0f, 0.9f, 1f);
    Color grapDashColor = new Color(0f, 0.9f, 1f);
    Color chainColor = new Color(0.45f, 0.45f, 0.45f);

    int rayLayerMask;

    bool isSwing = false;
    bool isSwingDashed = false;
    bool isGrap = false;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        distJoint = GetComponent<DistanceJoint2D>();
        parentRb = transform.parent.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();

        rayLayerMask = 1 << LayerMask.NameToLayer("Wall");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        distJoint.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
#if USE_GAMEMANAGER
        if (GameManager.Instance.GetDragable())
        {

            return;
        }
#endif
        if (EventSystem.current.IsPointerOverGameObject())
            return;


        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseDir = mousePos - transform.position;
        localAnchorPos = transform.InverseTransformPoint(anchorPos);

        float rayDist = 50f;
        hit = Physics2D.Raycast(transform.position, mouseDir, rayDist, rayLayerMask);

        if (Input.GetMouseButtonDown(0))
        {
            if(hit)
            {
                active = true;
                //if(!hit.transform.CompareTag(TagManager.tagGrapplingAble))
                if (hit.transform.GetComponent<GrapableBlock>() == null)
                {
                    StartSwing();
                }
                else
                {
                    StartGrapDash();
                }
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            EndSwing();
            EndGrapDash();
            active = false;
        }

        if(Input.GetMouseButtonDown(1))
        {
            SwingDash();
        }

        UpdateLineRenderer();
    }

    public void StartSwing()
    {
        // Swing
        if(!isSwing)
        {
            isSwing = true;

            anchorPos = hit.point;
            
            distJoint.enabled = true;
            distJoint.connectedAnchor = anchorPos;
            distJoint.distance = Vector2.Distance(anchorPos, transform.position);

            rb.linearVelocity = parentRb.linearVelocity;
            parentRb.linearVelocity = Vector2.zero;

            rb.mass = parentRb.mass;
            parentRb.mass = 0f;

            //col.enabled = true;

            //Vector2 tangent_a = new Vector2(localAnchorPos.y, -localAnchorPos.x);
            //Vector2 tangent_b = new Vector2(-localAnchorPos.y, localAnchorPos.x);
            //if (Vector2.Angle(tangent_a, rb.linearVelocity) < Vector2.Angle(tangent_b, rb.linearVelocity))
            //{
            //    rb.linearVelocity = tangent_a.normalized * rb.linearVelocity.magnitude;
            //}
            //else
            //{
            //    rb.linearVelocity = tangent_b.normalized * rb.linearVelocity.magnitude;
            //}
        }
    }

    public void EndSwing()
    {
        if(isSwing)
        {
            isSwing = false;
            isSwingDashed = false;

            lineRenderer.SetPosition(1, Vector3.zero);

            distJoint.enabled = false;

            //parentRb.linearDamping = orginDamping;

            parentRb.linearVelocity = rb.linearVelocity;
            rb.linearVelocity = Vector2.zero;

            parentRb.mass = rb.mass;
            rb.mass = 0f;

            //col.enabled = false;
        }
    }

    public void SwingDash()
    {
        float dashPower = 15f;
        if (isSwing && !isSwingDashed)
        {
            isSwingDashed = true;
            rb.linearVelocity = rb.linearVelocity.normalized * dashPower;
        }
    }
    
    public void StartGrapDash()
    {
        if(!isGrap)
        {
            isGrap = true;

            //grapCol = hit.collider;
            //grapCol.enabled = false;

            grapableBlock = hit.transform.GetComponent<GrapableBlock>();
            grapableBlock.GrapedChain();

            transform.parent.position = hit.transform.position;
            parentRb.linearVelocity = Vector2.zero;
        }
    }

    public void EndGrapDash()
    {
        float dashPower = 15f;
        if (isGrap)
        {
            isGrap = false;

            transform.parent.GetComponent<BallController>().HitBall(mouseDir.normalized * dashPower);
            //parentRb.linearVelocity = dir.normalized * 40f;

            //grapCol.enabled = true;
            grapableBlock.BreakBlock();
        }
    }
    
    void UpdateLineRenderer()
    {
        if(Input.GetMouseButton(0))
        {
            if(isSwing)
            {
                lineRenderer.SetPosition(1, localAnchorPos);

                // Color
                {
                    Color fullAlphaColor = chainColor;
                    fullAlphaColor.a = 1f;
                    lineRenderer.startColor = fullAlphaColor;

                    fullAlphaColor = chainColor;
                    fullAlphaColor.a = 1f;
                    lineRenderer.endColor = fullAlphaColor;
                }
                return;
            }
            else if(isGrap)
            {
                lineRenderer.SetPosition(1, mouseDir.normalized);   // Gage를 쓰는것도

                // Color
                {
                    Color fullAlphaColor = grapDashColor;
                    fullAlphaColor.a = 1f;
                    lineRenderer.startColor = fullAlphaColor;

                    fullAlphaColor = grapDashColor;
                    fullAlphaColor.a = 1f;
                    lineRenderer.endColor = fullAlphaColor;
                }

                return;
            }
        }
        if(hit)
        {
            lineRenderer.SetPosition(1, transform.InverseTransformPoint(hit.point));
            // Color
            {
                Color semiAlphaColor = aimColor;
                semiAlphaColor.a = 0.4f;
                lineRenderer.startColor = semiAlphaColor;

                semiAlphaColor = aimColor;
                semiAlphaColor.a = 0.4f;
                lineRenderer.endColor = semiAlphaColor;
            }
        }
        
    }

    public override void StopAbility()
    {
        //Debug.Log("Stop Ability()");
        active = false;
        EndSwing();
        EndGrapDash();
    }
}
