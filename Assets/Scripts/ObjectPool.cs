using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class ObjectPool : MonoBehaviour
{
#pragma warning disable 649
    private static Dictionary<Type, Queue<MonoBehaviour>> Pool = new Dictionary<Type, Queue<MonoBehaviour>>();
#pragma warning restore 649

    public T GetFromPool<T>(T requestedType, Vector3 position, Quaternion rotation, GameObject parent = null) where T : MonoBehaviour
    {
        if (Pool.ContainsKey(typeof(T)))
        {
            if (Pool[typeof(T)].Any())
            {
                var unpooledObject = Pool[typeof(T)].Dequeue();
                unpooledObject.transform.position = position;
                unpooledObject.transform.rotation = rotation;
                unpooledObject.transform.parent = parent != null ? parent.transform : transform;
                unpooledObject.gameObject.SetActive(true);
                return (T)unpooledObject;
            }
        }
        
        return InstantiateNewPoolObject(requestedType, position, rotation, parent);
    }

    public void ReturnToPool<T>(T returningObject) where T : MonoBehaviour
    {
        if (!Pool.ContainsKey(typeof(T)))
        {
            Pool.Add(typeof(T), new Queue<MonoBehaviour>());
        }
        
        returningObject.transform.position = transform.position;
        returningObject.transform.parent = transform;
        returningObject.gameObject.SetActive(false);
        
        Pool[typeof(T)].Enqueue(returningObject);
    }

    private T InstantiateNewPoolObject<T>(T requestedType, Vector3 position, Quaternion rotation, GameObject parent) where T : MonoBehaviour
    {
        return Instantiate(requestedType, position, rotation, parent != null ? parent.transform : transform);
    }
}
