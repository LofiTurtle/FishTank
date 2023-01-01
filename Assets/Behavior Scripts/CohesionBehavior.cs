using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // exit immediately if there are no neighbors
        if (context.Count == 0)
        {
            return Vector3.zero;
        }

        // average position of neighbors
        Vector3 cohesionMove = Vector3.zero;
        foreach (Transform item in context)
        {
            cohesionMove += item.position;
        }
        cohesionMove /= context.Count;

        // get offset vector from current agent position
        cohesionMove -= agent.transform.position;

        return cohesionMove;
    }
}
