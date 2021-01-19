using UnityEngine;

namespace Utilities.Extensions
{
    public static class ColorExtensions
    {
        public static Color ZeroAlpha(this Color originalColor)
        {
            var newColor = originalColor;
            newColor.a = 0;
            return newColor;
        } 
    }
}