using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyBlock : MonoBehaviour
{
    //private string tagBullet = "Bullet";
    //private string layerInvincible = "Invincible";

    public AudioClip sfx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TagManager.tagCoin))
        {
            float playSFXTime = 1f;
            SoundsPlayer.Instance.PlaySFX(sfx, 0.8f ,playSFXTime);

            //collision.gameObject.GetComponent<BulletController>().isDestroyed = true;
        }
    }

    //private IEnumerator LateDestroy(Collider2D collision)
    //{
    //    GameManager.Instance.canRetry = false;
    //    yield return new WaitForSeconds(1f);
    //    MakeBullet();
    //    GameManager.Instance.canRetry = true;
    //}

    //private void MakeBullet()
    //{
    //    PlayerController.Instance.MakeBullet();
    //    PlayerController.Instance.isBulletSelected = false;
    //    PlayerController.Instance.selectAvailable = true;
    //    PlayerController.Instance.bulletDestroyed = true;
    //    PlayerController.Instance.UseItem(PlayerController.Instance.selectedItem);
    //}

}
