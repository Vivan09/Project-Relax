using UnityEngine;

namespace SoftTetris
{
    public static class Utils
    {
        public static Vector3 ScreenToWorldPoint(Camera camera, Vector3 position)
        {
            position.z = camera.nearClipPlane;
            return camera.ScreenToWorldPoint(position);
        }

        public static float CalculateBottom(Vector3 position, int maxLength)
        {
            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, maxLength);
            
            if (hit)
                return Vector2.Distance(position, hit.point);

            return 0;
        }
    }
}