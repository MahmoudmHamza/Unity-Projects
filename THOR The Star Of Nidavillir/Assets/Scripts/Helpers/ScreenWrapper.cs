using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    private float colliderRadius;

    public float ColliderRadius
    {
        get
        {
            return colliderRadius;
        }
        set
        {
            colliderRadius = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        colliderRadius = GetComponent<CircleCollider2D>().radius;
    }

    /// <summary>
    /// Called when the game object becomes invisible to the camera
    /// </summary>
    void OnBecameInvisible()
    {
        Vector2 position = transform.position;

        if (position.x + colliderRadius > ScreenUtils.ScreenLeft ||
            position.x - colliderRadius < ScreenUtils.ScreenRight)
        {
            position.x *= -1;
        }

        if (position.y - colliderRadius > ScreenUtils.ScreenTop ||
            position.y + colliderRadius < ScreenUtils.ScreenBottom)
        {
            position.y *= -1;
        }

        // move ship
        transform.position = position;
    }
}
