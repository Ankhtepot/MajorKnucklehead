using UnityEngine;

namespace Interface
{
    public abstract class ConfigurationBase : ScriptableObject, IGameConfiguration
    {
        public abstract void ActivateConfiguration();
    }
}