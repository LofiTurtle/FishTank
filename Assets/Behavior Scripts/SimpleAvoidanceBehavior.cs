using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Simple Obstacle Avoidance")]
public class SimpleAvoidanceBehavior : FlockBehavior
{

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // check if agent is going to hit an obstacle
        RaycastHit hit;
        if (Physics.SphereCast(agent.transform.position, .15f,
            agent.transform.forward * 3, out hit, flock.neighborRadius, LayerMask.GetMask("Obstacle")))
        {
            return hit.normal;
        }
        else
        {
            return Vector3.zero;
        }
    }
}