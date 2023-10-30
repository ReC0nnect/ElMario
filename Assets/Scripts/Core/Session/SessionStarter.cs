using UnityEngine;


public class SessionStarter : MonoBehaviour
{
    SessionEntity Session;

    void Awake()
    {
        Session = SessionEntity.Create();
    }

    void Update()
    {
        Session.Update();
    }

    void FixedUpdate()
    {
        Session.FixedUpdate();
    }
}
