using UnityEngine;


[RequireComponent(typeof(Animator))]
public class CoinBrick : LevelObject, IHeadInteractable
{
    [SerializeField] Collider2D Collider;
    
    int Amount;

    Animator Animator;

    void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public void SetAmount(int amount)
    {
        Amount = amount;
    }

    public void GetCoin()
    {
        Animator.SetTrigger("OnHit");
        if (Amount <= 0)
        {
            return;
        }

        Debug.Log("Coin");
        if (--Amount <= 0)
        {
            Animator.SetBool("IsEmpty", true);
        }
    }

    void IHeadInteractable.Interact()
    {
        GetCoin();
    }
}