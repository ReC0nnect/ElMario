using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCounter
{
    HashSet<GameObject> GroundObjects = new();


    public static bool operator true(GroundedCounter counter) => counter.GroundObjects.Count > 0;
    public static bool operator false(GroundedCounter counter) => counter.GroundObjects.Count == 0;
    public static bool operator !(GroundedCounter counter) => counter.GroundObjects.Count == 0;

    public static GroundedCounter operator +(GroundedCounter counter, GameObject go)
    {
        counter.AddGround(go);
        return counter;
    }
    
    public static GroundedCounter operator -(GroundedCounter counter, GameObject go)
    {
        counter.RemoveGround(go);
        return counter;
    }

    void AddGround(GameObject go)
    {
        GroundObjects.Add(go);
    }

    void RemoveGround(GameObject go)
    {
        GroundObjects.Remove(go);
    }
}
