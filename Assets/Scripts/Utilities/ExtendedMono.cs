using UnityEngine;

namespace Utilities
{
    public class ExtendedMono : MonoBehaviour
    {
        private Transform _transform;

        protected new Transform transform
        {
            get
            {
                if (!_transform)
                {
                    _transform = base.transform;
                }

                return _transform;
            }
        }
    }
}