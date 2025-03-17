using Unity.VisualScripting;
using UnityEngine;

public class Eat : MonoBehaviour
{
    public AudioClip sfx;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag(TagManager.tagBall))
        {
            Destroy(transform.parent.gameObject);
            
            //GameManager.Instance.GainCoin();
            SoundManager.Instance.PlaySFX(sfx);
        }
    }
}
