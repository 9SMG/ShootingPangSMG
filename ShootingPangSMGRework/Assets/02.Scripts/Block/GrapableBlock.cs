using UnityEngine;

public class GrapableBlock : MonoBehaviour
{
    Collider2D col;
    SpriteRenderer renderer;
    

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        renderer = GetComponent<SpriteRenderer>();
    }
    public void GrapedChain()
    {
        col.enabled = false;
    }
    // Break
    public void BreakBlock()
    {
        renderer.enabled = false;
    }

    // Respawn
    public void RespawnBlock()
    {
        col.enabled = true;
        renderer.enabled = true;
    }
}
