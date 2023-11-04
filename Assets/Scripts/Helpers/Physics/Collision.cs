using System.Collections.Generic;
using UnityEngine;

namespace Physics
{
    public interface ICollision2DEnterHandler
    {
        void OnCollisionEnter(Collision2D collision, Collision collision2D);
    }

    public interface ICollision2DExitHandler
    {
        void OnCollisionExit(Collision2D collision, Collision collision2D);
    }

    public class Collision : MonoBehaviour
    {
        HashSet<ICollision2DEnterHandler> EnterHandlers = new();
        HashSet<ICollision2DExitHandler> ExitHandlers = new();

        public void SetEnterHandler(ICollision2DEnterHandler handler)
        {
            EnterHandlers.Add(handler);
        }

        public void SetExitHandler(ICollision2DExitHandler handler)
        {
            ExitHandlers.Add(handler);
        }

        public void RemoveEnterHandler(ICollision2DEnterHandler handler)
        {
            EnterHandlers.Remove(handler);
        }

        public void RemoveExitHandler(ICollision2DExitHandler handler)
        {
            ExitHandlers.Remove(handler);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            foreach (var handler in EnterHandlers)
            {
                handler.OnCollisionEnter(collision, this);
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            foreach (var handler in ExitHandlers)
            {
                handler.OnCollisionExit(collision, this);
            }
        }
    }
}