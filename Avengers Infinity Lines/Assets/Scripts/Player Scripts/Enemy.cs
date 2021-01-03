using UnityEngine;

public enum EnemyState
{
    Fixed,
    Scaling,
    ExtraScaling
}

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Animator enemyAnimator;

    private GameManager GameManager => GameManager.Instance;

    public Animator EnemyAnimator => this.enemyAnimator;

    private EnemyController enemyController;

    public void InitializeEnemy(EnemyState state, EnemyController controller)
    {
        this.enemyController = controller;
        this.AnimateEnemy(state);
    }

    private void AnimateEnemy(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Fixed:
                break;

            case EnemyState.Scaling:
                this.EnemyScaling();
                break;

            case EnemyState.ExtraScaling:
                this.EnemyExtraScaling();
                break;
        }
    }

    private void EnemyScaling()
    {
        this.enemyAnimator.SetBool("IsScalingUp", true);
    }

    private void EnemyExtraScaling()
    {
        this.enemyAnimator.SetBool("IsExtraScalingUp", true);
    }

    private void OnMouseDown()
    {
        if (!this.GameManager.IsSoulStoneEnabled)
        {
            return;
        }

        if(this.enemyController.CurrentDestroyedEnemies >= this.enemyController.MaxEnemiesToDestroy)
        {
            this.GameManager.IsSoulStoneEnabled = false;
            this.GameManager.UIController.HideHintPanel();
            return;
        }

        this.enemyController.DestroyEnemy(this, true);
        Destroy(this.gameObject);
    }
}
