using Shooter.Helper;
using Shooter.Services.SaveData;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Shooter.Repository
{
    public class SaveGeneratedDataRepository
    {
        private IData<NamedListOfSerializableVector3> _data;

        private string _folderName = "dataSave";
        protected string _filePrefix = "generatedData_";
        protected string _path;

        public SaveGeneratedDataRepository()
        {
            _data = new JsonData<NamedListOfSerializableVector3>();
            _path = Path.Combine(System.IO.Directory.GetCurrentDirectory(), _folderName);
        }

        public void Save(NamedListOfSerializableVector3 list)
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }

            _data.Save(list, GetFileName(list.Name));
        }

        public NamedListOfSerializableVector3 Load(string name)
        {
            if (!IsFileExists(name))
            {
                throw new FileNotFoundException($"Saved data {GetFileName(name)} not found!");
            }

            return _data.Load(GetFileName(name));
        }

        public bool IsFileExists(string name)
        {
            return File.Exists(GetFileName(name));
        }

        public string[] GetSavedFileNameArray()
        {
            var path = Path.Combine(_path);

            if (!Directory.Exists(path))
            {
                return new string[] { };
            }

            return Directory.GetFiles(path, $"{_filePrefix}*");
        }

        private string GetFileName(string name)
        {
            if (!name.Contains(_filePrefix))
            {
                name = $"{_filePrefix}{name}";
            }

            return Path.Combine(_path, name);
        }
    }
}
