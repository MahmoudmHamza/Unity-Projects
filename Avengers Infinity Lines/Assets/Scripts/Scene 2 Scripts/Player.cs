using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private Rigidbody2D rb;

    private Collider2D playerCollider;

    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
    }

    void Update () {
        if (Input.GetButtonDown("Start"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            Debug.Log("Game Started");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Destroy")
        {
            GameManager.Instance.PlayerOut = true;
            Destroy(this.gameObject);
            GameManager.Instance.DestroyAllFinishes();
            GameManager.Instance.DestroyAllObstacles();
            Debug.Log("hit box");
        }
        else if (other.tag == "Finish")
        {
            GameManager.Instance.PlayerOut = false;
            Destroy(this.gameObject);
            GameManager.Instance.UnRegisterFinish(other.gameObject);
            GameManager.Instance.DestroyAllObstacles();
            Debug.Log("you reached end line");
        }
        GameManager.Instance.SetCurrentState();
    }
}
