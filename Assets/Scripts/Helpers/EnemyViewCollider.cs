using UnityEngine;


public class EnemyViewCollider : MonoBehaviour
{
    EnemyView EnemyViewCached;
    public EnemyView EnemyView
    {
        get
        {
            if (EnemyViewCached == null)
            {
                EnemyViewCached = GetComponentInParent<EnemyView>();
            }
            return EnemyViewCached;
        }
    }
}
