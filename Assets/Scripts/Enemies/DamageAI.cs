using UnityEngine;

public class DamageAI : MonoBehaviour, IBotLogic, ISessionInit
{
    EnemyView Enemy;
    SessionEntity Session;

    public void Init(EnemyView enemy)
    {
        Enemy = enemy;
        Enemy.OnHeroHit += Hit;
    }

    void OnDestroy()
    {
        Enemy.OnHeroHit -= Hit;
    }

    void Hit()
    {
        Session.Player.Hit();
    }

    void ISessionInit.OnSessionInit(SessionEntity session)
    {
        Session = session;
    }
}