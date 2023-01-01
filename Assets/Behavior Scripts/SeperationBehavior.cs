using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Seperation")]
public class SeperationBehavior : FlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // exit immediately if there are no neighbors
        if (context.Count == 0)
        {
            return Vector3.zero;
        }

        // average position of neighbors
        Vector3 avoidanceMove = Vector3.zero;
        int avoidanceNeighbors = 0;
        foreach (Transform item in context)
        {
            if (Vector3.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                avoidanceNeighbors++;
                avoidanceMove += agent.transform.position - item.position;
            }
            
        }

        if (avoidanceNeighbors > 0)
        {
            avoidanceMove /= avoidanceNeighbors;
        }

        return avoidanceMove;
    }
}
