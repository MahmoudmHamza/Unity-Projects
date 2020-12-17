using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    private float angle;
    private float magnitude;

    /// <summary>
    /// Initialize the asteroid and moving it.
    /// </summary>
    /// <param name="direction">asteroid's direction</param>
    /// <param name="location">asteroid's location</param>
    public void Initialize(Direction direction, Vector3 location)
    {
        // set the asteroid to the passed location
        this.transform.position = location;

        // apply impulse force to get game object moving
        this.angle = this.GetAngleBasedOnDirection(direction) * Mathf.Deg2Rad;
        this.StartMoving(this.gameObject, this.angle);
    }

    /// <summary>
    /// Moving the asteroid
    /// </summary>
    private void StartMoving(GameObject obj, float angle)
    {
        var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        this.magnitude = Random.Range(GameConstants.MinImpulseForce, GameConstants.MaxImpulseForce);

        obj.GetComponent<Rigidbody2D>().AddForce(
            direction * magnitude,
            ForceMode2D.Impulse);
    }

    /// <summary>
    /// Gets the angle random value based on input direction
    /// </summary>
    /// <param name="direction">input direction</param>
    /// <returns>random angle</returns>
    private float GetAngleBasedOnDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Random.Range(GameConstants.MinUpRange, GameConstants.MaxUpRange);
            case Direction.Down:
                return Random.Range(GameConstants.MinDownRange, GameConstants.MaxDownRange);
            case Direction.Left:
                return Random.Range(GameConstants.MinLeftRange, GameConstants.MaxLeftRange);
            case Direction.Right:
                return Random.Range(GameConstants.MinRightRange, GameConstants.MaxRightRange);
            default:
                break;
        }
        return 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Star")
        {
            AudioManager.Instance.Play(AudioClipName.AsteroidExplosion);
            GameManager.Instance.UnregisterAsteroid(this.gameObject);
        }
    }

    /// <summary>
    /// Shrink the asteroid when it gets hit by bullet
    /// </summary>
    private void SplitAndShrinkAsteroid()
    {
        Vector3 scale = this.transform.localScale;
        float radius = this.GetComponent<ScreenWrapper>().ColliderRadius;

        for(int i = 0; i < GameConstants.ShrinkedAsteroidCount; i++)
        {
            this.CreateShrinkedAsteroid(scale, radius);
        }

        Destroy(gameObject);
    }

    private void CreateShrinkedAsteroid(Vector3 scale, float radius)
    {
        GameObject miniAsteroid = Instantiate(gameObject);
        miniAsteroid.transform.localScale = new Vector3(scale.x / 2, scale.y / 2, scale.z);

        radius /= 2;
        miniAsteroid.GetComponent<CircleCollider2D>().radius = radius;
        miniAsteroid.GetComponent<ScreenWrapper>().ColliderRadius = radius;

        var angle = Random.Range(0, 2 * Mathf.PI);
        this.StartMoving(miniAsteroid, angle);
    }
}
