using Shooter.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

namespace Shooter.Helper
{
    public class ObjectGenerator : MonoBehaviour
    {
        private const string parentObjectTag = "OG_Parent";

        public GameObject objectToClone;
        public int numberOfObjects = 10;

        private float offset = 0.2f;
        private float step = 2f;
        private float obstacleCheckRadius;
        private Random random = new Random();
        private bool tagIsCreated;

        private SaveGeneratedDataRepository SaveGeneratedDataRepository = new SaveGeneratedDataRepository();

        #region StorageOperations

        public void SaveGenerated(string gameObjectName)
        {
            var gameObject = GameObject.Find(gameObjectName);

            if (!gameObject)
            {
                Debug.LogError($"Save Error! Game object with name '{gameObjectName}' not found");

                return;
            }

            NamedListOfSerializableVector3 saveData = new NamedListOfSerializableVector3(
                gameObjectName,
                new List<SerializableVector3>()
            );

            foreach (Transform child in gameObject.transform)
            {
                saveData.List.Add(
                    new SerializableVector3(
                        child.transform.position.x,
                        child.transform.position.y,
                        child.transform.position.z
                    )
                );
            }

            SaveGeneratedDataRepository.Save(saveData);
        }

        public void LoadGenerated(string savedFileName)
        {
            try
            {
                SpawnObjectAtPositionFromLIst(SaveGeneratedDataRepository.Load(savedFileName));
            }
            catch (FileNotFoundException)
            {
                Debug.LogError($"Load Error! Game object with name '{savedFileName}' not found");
            }
        }

        public void DeleteGenerated(string gameObjectName)
        {
            var gameObject = GameObject.Find(gameObjectName);

            if (!gameObject)
            {
                Debug.LogError($"Delete Error! Game object with name '{gameObjectName}' not found");
            }
            else
            {
                GameObject.DestroyImmediate(gameObject);
            }
        }

        public string[] GetSavedFileNameArray()
        {
            return SaveGeneratedDataRepository.GetSavedFileNameArray();
        }

        public bool IsSavedDataExists()
        {
            return SaveGeneratedDataRepository.GetSavedFileNameArray().Length > 0;
        }

        private void SpawnObjectAtPositionFromLIst(NamedListOfSerializableVector3 list)
        {
            GameObject parentGameObject = GreateParentGameObject(PrepareParentObjectName(list.Name));

            foreach (var position in list.List)
            {
                Instantiate(
                  GetObjectToClone(),
                   position,
                   Quaternion.identity,
                   parentGameObject.transform
                   );
            }
        }

        #endregion

        #region CoreLogic

        public GameObject[] FindAllGeneratedObjects()
        {
            CreateTagIfNotExists();

            return GameObject.FindGameObjectsWithTag(parentObjectTag);
        }

        public void GenerateObjects()
        {
            CreateTagIfNotExists();

            GameObject[] Grounds = GameObject.FindGameObjectsWithTag("Ground")
                .Where(c => c.transform.rotation.x == 0 && c.transform.rotation.y == 0 && c.transform.rotation.z == 0)
                .ToArray();

            List<Vector3> spawnPositionList = new List<Vector3>();
            foreach (GameObject ground in Grounds)
            {
                spawnPositionList.AddRange(GetSpawnPositionsList(ground));
            }

            if (spawnPositionList.Count > 0)
            {
                SpawnObjectAtRandomPositionFromList(spawnPositionList);
            }
        }

        private void SpawnObjectAtRandomPositionFromList(List<Vector3> spawnPositionList)
        {
            GameObject parentGameObject = GreateParentGameObject(PrepareParentObjectName());

            var objectsLimit = Math.Min(spawnPositionList.Count, numberOfObjects);

            for (int i = 0; i < objectsLimit; i++)
            {
                Instantiate(
                    GetObjectToClone(),
                    spawnPositionList[random.Next(spawnPositionList.Count)],
                    Quaternion.identity,
                    parentGameObject.transform
                    );
            }
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
            var initialPositionY = ground.transform.position.y + sizeY / 2 + objSizeY / 2;

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

        #endregion

        #region Helpers

        private GameObject GreateParentGameObject(string name)
        {
            var parentGameObject = new GameObject(name);
            parentGameObject.tag = parentObjectTag;

            return parentGameObject;
        }

        private GameObject GetObjectToClone()
        {
            return objectToClone ? objectToClone : objectToClone = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }

        private string PrepareParentObjectName(string name = null)
        {
            return name != null ? name : $"OG_{objectToClone.name}_box_{DateTime.Now.ToString("yyyyMMddTHHmmss.fffffff")}";
        }

        private bool CreateTagIfNotExists()
        {
            if (tagIsCreated)
            {
                return true;
            }

            tagIsCreated = TagCreator.CreateTag(parentObjectTag);

            return true;
        }

        #endregion
    }
}
