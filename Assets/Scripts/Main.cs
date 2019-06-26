using Shooter.Controller;
using Shooter.Interface;
using Shooter.Model;
using Shooter.Model.Ai;
using Shooter.Repository;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public sealed class Main : MonoBehaviour
    {
        public FlashLightController FlashLightController { get; private set; }
        public InputController InputController { get; private set; }
        public PlayerController PlayerController { get; private set; }
        public ObjectDetectorController ObjectDetectorController { get; private set; }
        public WeaponController WeaponController { get; private set; }
        public BotController BotController { get; private set; }
        public SaveDataRepository SaveDataRepository { get; private set; }
        public Transform Player;
        public Transform SpawnPoint;
        public ObjectManager ObjectManager;
		public Bot RefBotPrefab;

        private readonly List<IOnUpdate> _updates = new List<IOnUpdate>();

        public static Main Instance { get; private set; }

        private void Awake()
        {
            Instance = this;

            Player = GameObject.FindGameObjectWithTag("Player").transform;

            SaveDataRepository = new SaveDataRepository();

            ObjectManager = new ObjectManager();

            PlayerController = new PlayerController(new UnitMotor(Player));
            _updates.Add(PlayerController);

            FlashLightController = new FlashLightController();
            _updates.Add(FlashLightController);

            InputController = new InputController();
            _updates.Add(InputController);

            ObjectDetectorController = new ObjectDetectorController(Camera.main);
            _updates.Add(ObjectDetectorController);

            WeaponController = new WeaponController();
            _updates.Add(WeaponController);

            BotController = new BotController();
            _updates.Add(BotController);
        }

        private void Start()
        {
            ObjectManager.OnStart();
            FlashLightController.OnStart();
            ObjectDetectorController.OnStart();
            InputController.On();
            BotController.OnStart();
            BotController.On();
        }

        private void Update()
        {
            for (var i = 0; i < _updates.Count; i++)
            {
                _updates[i].OnUpdate();
            }
        }
    }
}
