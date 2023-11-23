using UnityEngine;
using UnityEngine.UI;


public class DeathScreen : MonoBehaviour, ISessionInit
{
    [SerializeField] GameObject Panel;
    [SerializeField] Button ReplayButton;

    SessionEntity Session;

    void Awake()
    {
        ReplayButton.onClick.RemoveAllListeners();
        ReplayButton.onClick.AddListener(DoReplay);
        Panel.SetActive(false);
    }

    void DoReplay()
    {
        Session.Replay();
    }

    public void Show()
    {
        Panel.SetActive(true);
    }

    void ISessionInit.OnSessionInit(SessionEntity session)
    {
        Session = session;
    }
}
