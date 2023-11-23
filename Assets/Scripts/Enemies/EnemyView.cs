using Cysharp.Threading.Tasks;
using Physics;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyView : MonoBehaviour, ITrigger2DEnterHandler, ISessionInit
{
    [SerializeField] Animator Animator;
    [SerializeField] Trigger2D TriggerHero;
    [SerializeField] Trigger2D TriggerWorld;
    [SerializeField] float MoveSpeed = 5f;
    [SerializeField] float DeathDisappearDelay = 1f;

    Rigidbody2D Rigidbody;
    Vector2 Direction;
    bool IsDead;

    public Vector2 Position => transform.position;

    SessionEntity Session;

    public event Action OnHeroHit = delegate { };

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        var botLogics = GetComponents<IBotLogic>();
        for (int i = 0; i < botLogics.Length; i++)
        {
            botLogics[i].Init(this);

            if (botLogics[i] is ISessionInit sessionInit)
            {
                sessionInit.OnSessionInit(Session);
            }
        }
        TriggerHero.SetEnterHandler(this);
        TriggerWorld.SetEnterHandler(this);
    }

    void OnDestroy()
    {
        TriggerHero.RemoveEnterHandler(this);
        TriggerWorld.RemoveEnterHandler(this);
    }

    public void SetDirection(Vector2 direction)
    {
        if (IsDead) { return; }

        Direction = direction;
    }

    public void Move()
    {
        if (IsDead) { return; }

        Rigidbody.velocity = MoveSpeed * Direction;
    }

    public async void AttackFromAbove()
    {
        if (IsDead) { return; }

        IsDead = true;
        Animator.SetTrigger("OnDeath");
        await UniTask.Delay(TimeSpan.FromSeconds(DeathDisappearDelay), ignoreTimeScale: false);
        Destroy(gameObject);
    }

    void ITrigger2DEnterHandler.OnTriggerEnter(Collider2D collision, Trigger2D trigger)
    {
        if (IsDead) { return; }

        if (trigger == TriggerHero)
        {
            OnHeroHit.Invoke();
        }
        else if (trigger == TriggerWorld)
        {
            Direction.x *= -1f;
        }
    }

    void ISessionInit.OnSessionInit(SessionEntity session)
    {
        Session = session;
    }
}
