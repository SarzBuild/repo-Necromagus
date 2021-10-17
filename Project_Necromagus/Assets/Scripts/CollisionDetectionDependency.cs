using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectionDependency : MonoBehaviour
{
    public LayerMask PlayerLayerMask;
    public LayerMask DeadBodyLayerMask;
    
    public RaycastHit2D CheckForObject(Collider2D _collider2D, Vector2 direction, LayerMask passLayerMask, float RayMaxDist)
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            _collider2D.bounds.center, 
            _collider2D.bounds.size, 
            0f, 
            direction, 
            RayMaxDist,
            passLayerMask
        );
        return hit;
    }
}
