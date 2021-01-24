using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

namespace Utilities.ObjectPool
{
    public class ObjectPool : MonoBehaviour
    {
#pragma warning disable 649
        public List<PreSpawnSetItem> preSpawnSetItems;
        private static readonly Dictionary<Type, Queue<MonoBehaviour>> Pool = new Dictionary<Type, Queue<MonoBehaviour>>();
#pragma warning restore 649

        private void Start()
        {
            if (preSpawnSetItems.Any())
            {
                SpawnPreSpawnItems();
            }
        }

        public T GetFromPool<T>(T requestedType, Vector3 position, Quaternion rotation, GameObject parent = null) where T : MonoBehaviour
        {
            if (Pool.ContainsKey(typeof(T)) && Pool[typeof(T)].Any())
            {
                var removedObject = Pool[typeof(T)].Dequeue() as T;
                // ReSharper disable once PossibleNullReferenceException
                var removedObjectTransform = removedObject.transform;
                removedObjectTransform.position = position;
                removedObjectTransform.rotation = rotation;
                removedObjectTransform.parent = parent != null ? parent.transform : transform;
                removedObject.gameObject.SetActive(true);
                return ProcessInterfaces(removedObject);
            }
        
            return InstantiateNewPoolObject(requestedType, position, rotation, parent);
        }

        public void ReturnToPool<T>(T returningObject) where T : MonoBehaviour
        {
            if (!Pool.ContainsKey(typeof(T)))
            {
                Pool.Add(typeof(T), new Queue<MonoBehaviour>());
            }

            var returningObjectTransform = returningObject.transform;
            returningObjectTransform.position = transform.position;
            returningObjectTransform.parent = transform;
            returningObject.gameObject.SetActive(false);
        
            Pool[typeof(T)].Enqueue(returningObject);
        }

        private T InstantiateNewPoolObject<T>(T requestedType, Vector3 position, Quaternion rotation, GameObject parent, bool instantiateToDisabled = false) where T : MonoBehaviour
        {
            var newObject = ProcessInterfaces(Instantiate(requestedType, position, rotation, parent != null ? parent.transform : transform));

            if (instantiateToDisabled)
            {
                newObject.gameObject.SetActive(false);
            }
                
            return newObject;
        }
        
        private T InstantiateNewPoolObject<T>(T objectToInstantiate, bool instantiateToDisabled = false) where T : MonoBehaviour
        {
            var objectToInstantiateTransform = objectToInstantiate.transform;
            return InstantiateNewPoolObject(
                objectToInstantiate, objectToInstantiateTransform.position, objectToInstantiateTransform.rotation, gameObject, instantiateToDisabled
            );
        }
        
        private T ProcessInterfaces<T>(T target) where T : MonoBehaviour
        {
            if (target is IPoolNeedy needy)
            {
                if (!needy.pool)
                {
                    needy.pool = this;
                }
            }

            if (target is IPoolInitializable initializable)
            {
                initializable.Initialize();
            }

            return target;
        }
        
        private void SpawnPreSpawnItems()
        {
            preSpawnSetItems.ForEach(item =>
            {
                if (!item.prefabGameObject || item.howMany <= 0) return;

                for (int i = 0; i < item.howMany; i++)
                {
                    var newObject = InstantiateNewPoolObject(item.prefabGameObject, true);
                    ReturnToPool(newObject);
                }
            });
        }

        [Serializable]
        public class PreSpawnSetItem
        {
            public MonoBehaviour prefabGameObject;
            public int howMany;
        }
    }
}
