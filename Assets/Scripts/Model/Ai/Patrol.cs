using UnityEngine;
using UnityEngine.AI;

namespace Shooter.Model.Ai
{
    public static class Patrol
    {
        public static Vector3 GenericPoint(Transform agent)
        {
            Vector3 result;

            var dis = Random.Range(5, 50);
            var randomPoint = Random.insideUnitSphere * dis;

            NavMesh.SamplePosition(agent.position + randomPoint, out var hit, dis, NavMesh.AllAreas);
            result = hit.position;

            return result;
        }
    }
}