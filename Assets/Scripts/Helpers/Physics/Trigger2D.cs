using System.Collections.Generic;
using UnityEngine;

namespace Physics
{
    public interface ITrigger2DEnterHandler
    {
        void OnTriggerEnter(Collider2D collision, Trigger2D trigger2D);
    }

    public interface ITrigger2DExitHandler
    {
        void OnTriggerExit(Collider2D collision, Trigger2D trigger2D);
    }

    public class Trigger2D : MonoBehaviour
    {
        HashSet<ITrigger2DEnterHandler> EnterHandlers = new();
        HashSet<ITrigger2DExitHandler> ExitHandlers = new();

        public void SetEnterHandler(ITrigger2DEnterHandler handler)
        {
            EnterHandlers.Add(handler);
        }

        public void SetExitHandler(ITrigger2DExitHandler handler)
        {
            ExitHandlers.Add(handler);
        }

        public void RemoveEnterHandler(ITrigger2DEnterHandler handler)
        {
            EnterHandlers.Remove(handler);
        }

        public void RemoveExitHandler(ITrigger2DExitHandler handler)
        {
            ExitHandlers.Remove(handler);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            foreach (var handler in EnterHandlers)
            {
                handler.OnTriggerEnter(collision, this);
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            foreach (var handler in ExitHandlers)
            {
                handler.OnTriggerExit(collision, this);
            }
        }
    }
}
