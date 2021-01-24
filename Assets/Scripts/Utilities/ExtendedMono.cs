using UnityEngine;

namespace Utilities
{
    public class ExtendedMono : MonoBehaviour
    {
        private Transform _transform;

        public new Transform transform
        {
            get
            {
                if (_transform)
                {
                    _transform = base.transform;
                }

                return _transform;
            }
        }
    }
}