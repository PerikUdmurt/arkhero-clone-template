﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetFinder
{ 
    public bool FindNearestVisibleTarget(Collider collider, float range, LayerMask targetLayer, out GameObject target)
    {
        target = null;
        List<Collider> colliders = FindAllColliders(collider, range, targetLayer);
        if (colliders.Count == 0) return false;

        /*
        var visibleTargets = from c in colliders
                   where !Physics.Linecast(collider.bounds.center, c.transform.position,)
                   select c;
        */

        var nearestTargets = from c in colliders
                      orderby (c.transform.position - collider.transform.position).magnitude
                      select c;


        target = nearestTargets.FirstOrDefault().gameObject;
        if (target == null) return false;
        return true;
    }

    public bool FindNearestTarget(Collider collider, float range, LayerMask targetLayer, out GameObject target)
    {
        target = null;
        List<Collider> colliders = FindAllColliders(collider, range, targetLayer);
        if (colliders.Count == 0) return false;

        target = colliders.First().gameObject;
        return true;
    }

    private List<Collider> FindAllColliders(Collider collider, float range, LayerMask layerMask)
    {
        List<Collider> colliders = Physics.OverlapSphere(collider.bounds.center, range, layerMask).ToList();
        return colliders;
    }
}
