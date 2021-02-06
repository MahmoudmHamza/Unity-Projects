using UnityEngine;

/// <summary>
/// Makes the main camera follow the player
/// </summary>
public class FollowPlayer : MonoBehaviour
{
    private Camera playerCamera;

    void Start()
    {
        this.playerCamera = Camera.main;
    }

    private void LateUpdate()
    {
        this.HandleCameraFollowing();
    }

    private void HandleCameraFollowing()
    {
        this.playerCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.playerCamera.transform.position.z);
    }
}
