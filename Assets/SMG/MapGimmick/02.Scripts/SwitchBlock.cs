using UnityEngine;

public class SwitchBlock : MonoBehaviour
{
    public bool isOn = false;
    public AudioClip sfx;

    Color32 offColor = new Color32(255, 0, 0, 255);
    Color32 onColor = new Color32(0, 255, 0, 255);

    SpriteRenderer child;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        if (renderers.Length > 1)
        {
            child = renderers[1];
            child.color = isOn ? onColor : offColor;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOn = !isOn;
        child.color = isOn ? onColor : offColor;

        SoundsPlayer.Instance.PlaySFX(sfx);
    }
}
