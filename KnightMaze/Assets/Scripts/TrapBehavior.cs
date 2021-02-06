using System.Collections;
using UnityEngine;

public class TrapBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject flamesPrefab;

    [SerializeField]
    private AudioSource trapAudioSource;

    [SerializeField]
    private float activationTime;

    private GameManager gameManager => GameManager.Instance;

    private void Awake()
    {
        this.gameManager.OnGameEnded += this.OnGameEnded;
    }

    private void Start()
    {
        this.ToggleTrap(false);
        this.StartCoroutine(this.TrapActivationCoroutine());
    }

    private IEnumerator TrapActivationCoroutine()
    {
        yield return new WaitForSeconds(this.activationTime);
        this.ToggleTrap(true);

        yield return new WaitForSeconds(this.activationTime);
        this.ToggleTrap(false);

        this.StartCoroutine(this.TrapActivationCoroutine());
    }

    private void ToggleTrap(bool state)
    {
        this.flamesPrefab.SetActive(state);
        this.trapAudioSource.enabled = state;
    }

    private void OnGameEnded(GameStatus status)
    {
        this.StopAllCoroutines();
        this.ToggleTrap(false);
    }

    private void OnDestroy()
    {
        if(this.gameManager != null)
        {
            this.gameManager.OnGameEnded -= this.OnGameEnded;
        }
    }
}
