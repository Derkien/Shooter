using Shooter.Helper;
using Shooter.Services.SaveData;
using System.IO;
using UnityEngine;

namespace Shooter.Repository
{
    public class SaveDataRepository
    {
        private IData<SerializableGameObject> _data;

        private string _folderName = "dataSave";
        protected string _fileName = "data.bat";
        protected string _path;

        public SaveDataRepository()
        {
            _data = new JsonData<SerializableGameObject>();
            _path = Path.Combine(Application.dataPath, _folderName);
        }

        public void Save()
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }

            var player = new SerializableGameObject
            {
                Pos = Main.Instance.Player.position,
                Name = Main.Instance.Player.name,
                IsEnable = true
            };

            _data.Save(player, Path.Combine(_path, _fileName));
        }

        public void Load()
        {
            if (!IsDataExists())
            {
                return;
            }

            var file = Path.Combine(_path, _fileName);

            var newPlayer = _data.Load(file);
            Main.Instance.Player.position = newPlayer.Pos;
            Main.Instance.Player.name = newPlayer.Name;
            Main.Instance.Player.gameObject.SetActive(newPlayer.IsEnable);

            Debug.Log(newPlayer);
        }

        public virtual bool IsDataExists()
        {
            var file = Path.Combine(_path, _fileName);

            return File.Exists(file);
        }
    }
}
