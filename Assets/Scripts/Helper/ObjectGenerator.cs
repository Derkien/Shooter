using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

namespace Shooter
{
    public class ObjectGenerator : MonoBehaviour
    {
        public GameObject objectToClone;
        public int numberOfObjects = 10;

        private int generatedSeriesNumber = 1;
        private float offset = 0.2f;
        private float step = 2f;
        private float obstacleCheckRadius;
        private Random random = new Random();

        public void GenerateObjects()
        {
            if (!objectToClone)
            {
                objectToClone = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            }

            GameObject[] Grounds = GameObject.FindGameObjectsWithTag("Ground")
                .Where(c => c.transform.rotation.x == 0 && c.transform.rotation.y == 0 && c.transform.rotation.z == 0)
                .ToArray();

            List<Vector3> spawnPositionsList = new List<Vector3>();
            foreach (GameObject ground in Grounds)
            {
                spawnPositionsList.AddRange(GetSpawnPositionsList(ground));
            }

            if (spawnPositionsList.Count > 0)
            {
                SpawnObject(objectToClone, spawnPositionsList);
            }
        }

        private void SpawnObject(GameObject ground, List<Vector3> spawnPositionsList)
        {
            var parentGameObject = new GameObject($"OG: {objectToClone.name} ({generatedSeriesNumber})");

            var objectsLimit = Math.Min(spawnPositionsList.Count, numberOfObjects);

            for (int i = 0; i < objectsLimit; i++)
            {
                Instantiate(
                    objectToClone,
                    spawnPositionsList[random.Next(spawnPositionsList.Count)],
                    Quaternion.identity,
                    parentGameObject.transform
                    );
            }

            generatedSeriesNumber++;
        }

        private List<Vector3> GetSpawnPositionsList(GameObject ground)
        {
            var sizeX = ground.transform.localScale.x;
            var sizeZ = ground.transform.localScale.z;
            var sizeY = ground.transform.localScale.y;

            var objSizeX = this.objectToClone.transform.localScale.x;
            var objSizeZ = this.objectToClone.transform.localScale.z;
            var objSizeY = this.objectToClone.transform.localScale.y;

            var diametr = (float)(Math.Max(objSizeX, objSizeZ) + 0.2);
            var radius = diametr / 2;

            var countX = (sizeX - 2 * offset) / diametr / step;
            var countZ = (sizeZ - 2 * offset) / diametr / step;

            var initialPositionX = ground.transform.position.x + sizeX / 2 - radius;
            var initialPositionZ = ground.transform.position.z + sizeZ / 2 - radius;
            var initialPositionY = ground.transform.position.y + sizeY / 2 + objSizeY/2;

            var currentPositionX = initialPositionX;

            List<Vector3> validPositionsList = new List<Vector3>();

            for (int i = 0; i < countX; i++)
            {
                var currentPositionZ = initialPositionZ;

                for (int j = 0; j < countZ; j++)
                {
                    var position = new Vector3(currentPositionX, initialPositionY, currentPositionZ);

                    Collider[] colliders = Physics.OverlapSphere(position, radius).Where(c => c.tag != "Ground").ToArray();

                    var hitResult = NavMesh.SamplePosition(position, out NavMeshHit hit, 0.5f, NavMesh.AllAreas);

                    var isValidPosition = colliders.Length == 0 && hitResult;

                    if (isValidPosition)
                    {
                        validPositionsList.Add(position);
                    }

                    currentPositionZ -= diametr * step;
                }

                currentPositionX -= diametr * step;
            }

            return validPositionsList;
        }
    }
}
