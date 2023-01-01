using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Obstacle Avoidance")]
public class AvoidanceBehavior : FlockBehavior
{
    private static Vector3[] rayDirections = new Vector3[100];

    private void calculateRayDirections()
    {
        // pi * 2 * goldenRatio
        float angleStep = Mathf.PI * 2 * (1 + Mathf.Sqrt(5)) / 2;

        // loop through each
        for (int i = 0; i < rayDirections.Length; i++)
        {
            float inclination = Mathf.Acos(1 - 2 * i / rayDirections.Length);
            float azimuth = angleStep * i;

            float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            float z = Mathf.Cos(inclination);

            rayDirections[i] = new Vector3(x, y, z);
        }
    }
    
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (rayDirections[0].Equals(Vector3.zero))
        {
            calculateRayDirections();
        }
        // check if agent is going to hit an obstacle
        RaycastHit hit;
        // agent.AgentCollider.bounds.size.magnitude  <- radius?
        if (Physics.SphereCast(agent.transform.position, 0.15f,
            agent.transform.forward, out hit, flock.neighborRadius * 3, LayerMask.GetMask("Obstacle")))
        {
            foreach (Vector3 rayDirection in rayDirections)
            {
                Vector3 dir = agent.transform.TransformDirection(rayDirection);
                Ray ray = new Ray(agent.transform.position, dir);
                if (!Physics.SphereCast(ray, 0.15f, flock.neighborRadius * 5, LayerMask.GetMask("Obstacle")))
                {
                    //Debug.Log(agent + " avoiding with ray direction " + dir);
                    return dir;
                }
            }
        }
        // not sure which of these is the correct one to return. Prob zero, because this is used with others in CompositeBehavior
        return Vector3.zero;
        //return agent.transform.forward;
    }
}
