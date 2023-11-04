using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Brick : LevelObject, IHeadInteractable
{
    [SerializeField] Collider2D Collider;
    [SerializeField] float DestroyDelay;

    Animator Animator;

    void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    async public void DoDestroy()
    {
        Animator.SetTrigger("OnDestroy");
        await UniTask.Delay((int)(DestroyDelay * 1000f));
        Destroy(gameObject);
    }

    void IHeadInteractable.Interact()
    {
        DoDestroy();
    }
}
