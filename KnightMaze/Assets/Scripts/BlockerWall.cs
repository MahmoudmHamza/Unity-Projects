using UnityEngine;

/// <summary>
/// Controls faker blocker osbtacle behavior
/// </summary>
public class BlockerWall : MonoBehaviour
{
    [SerializeField]
    private GameObject destructionEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            AudioManager.Instance.PlaySoundEffect(AudioKey.BlockDestruction);
            Instantiate(this.destructionEffect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}