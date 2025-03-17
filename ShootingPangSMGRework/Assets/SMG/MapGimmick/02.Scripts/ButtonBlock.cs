using UnityEngine;

public class ButtonBlock : MonoBehaviour
{
    public bool isPush;
    public AudioClip sfx;

    Color32 offColor = new Color32(0, 150, 150, 255);
    Color32 onColor = new Color32(0, 255, 255, 255);

    SpriteRenderer child;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPush = false;
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        if (renderers.Length > 1)
        {
            child = renderers[1];
            child.color = offColor;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isPush = true;
        child.color = onColor;
        SoundsPlayer.Instance.PlaySFX(sfx);
    }
}
