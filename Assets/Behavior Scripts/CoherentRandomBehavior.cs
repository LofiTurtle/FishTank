using System.Collections;
using System.Collections.Generic;
using Assets.Utilities;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Coherent Random Movement")]
public class CoherentRandomBehavior : FlockBehavior
{
    public float animationSpeed = 0.1f;
    public float noiseScale = 1f;
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // calculate 4D -> "3D" noise. vary seed per-flock and per-axis
        Vector3 scaledPosition = agent.transform.position * noiseScale;
        float noiseX = OpenSimplex.Noise4_ImproveXYZ(flock.GetInstanceID(), scaledPosition.x, scaledPosition.y, scaledPosition.z,
            Time.time * animationSpeed);
        float noiseY = OpenSimplex.Noise4_ImproveXYZ(flock.GetInstanceID() + 1, scaledPosition.x, scaledPosition.y, scaledPosition.z,
            Time.time * animationSpeed);
        float noiseZ = OpenSimplex.Noise4_ImproveXYZ(flock.GetInstanceID() + 2, scaledPosition.x, scaledPosition.y, scaledPosition.z,
            Time.time * animationSpeed);

        return new Vector3(noiseX, noiseY, noiseZ);
    }
}