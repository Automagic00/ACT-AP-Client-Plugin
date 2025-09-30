using System.Reflection;
using UnityEngine;

namespace ACTAP.Extensions
{
    public static class AreaMapExt
    {
        public static void Triangulate(this AreaMap areaMap, RectTransform icon, Vector3 worldPosition, Vector2 worldBottomLeft, Vector2 worldTopRight, Vector2 mapBottomLeft, Vector2 mapTopRight, bool isPlayer = false, bool centered = false)
        {
            MethodInfo triangulate = areaMap.GetType().GetMethod("Triangulate", BindingFlags.NonPublic | BindingFlags.Instance);
            triangulate.Invoke(areaMap, new object[] { icon, worldPosition, worldBottomLeft, worldTopRight, mapBottomLeft, mapTopRight, isPlayer, centered });
        }
    }
}
