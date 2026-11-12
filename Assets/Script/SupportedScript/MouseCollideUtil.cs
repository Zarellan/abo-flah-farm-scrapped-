using UnityEngine;

public static class MouseCollideUtil
{
    public static bool CollisionDetectorMouse(Vector3 mousePosition, Transform target,BoxCollider2D collider)
    {
        if (collider == null)
        {
            return false;
        }

        Vector2 colliderSize = collider.size;
        Vector2 colliderCenter = target.position;

        Vector2 objectMin = colliderCenter - colliderSize / 2f;
        Vector2 objectMax = colliderCenter + colliderSize / 2f;

        return mousePosition.x >= objectMin.x &&
           mousePosition.x <= objectMax.x &&
           mousePosition.y >= objectMin.y &&
           mousePosition.y <= objectMax.y;
    }
}
