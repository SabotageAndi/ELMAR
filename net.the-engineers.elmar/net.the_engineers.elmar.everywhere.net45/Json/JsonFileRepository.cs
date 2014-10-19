using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using net.the_engineers.elmar.everywhere.net45.Database;

namespace net.the_engineers.elmar.everywhere.net45.Json
{
    public interface IJsonFileRepository<T>
    {
        T Create();
        void Save(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
    }

    public interface IIdJsonFileRepository<T> : IJsonFileRepository<T>
    {
        T GetById(int id);
    }


    public class JsonFileRepository<T> : IJsonFileRepository<T> where T : new()
    {
        private readonly string _fileName;

        protected object _lockData;
        protected List<T> _data;

        public JsonFileRepository(string fileName)
        {
            _lockData = new object();

            _fileName = fileName;
        }

        public virtual T Create()
        {
            return new T();
        }

        public void Save(T entity)
        {
            LoadData();

            if (!_data.Contains(entity))
                _data.Add(entity);

            SaveData();
        }

        public void Delete(T entity)
        {
            LoadData();

            _data.Remove(entity);

            SaveData();
        }

        public IEnumerable<T> GetAll()
        {
            LoadData();

            return _data;
        }

        protected void LoadData()
        {
            lock (_lockData)
            {
                if (_data != null)
                    return;

                if (!File.Exists(_fileName))
                {
                    _data = new List<T>();
                    return;
                }

                var fileContent = File.ReadAllText(_fileName);

                _data = JsonConvert.DeserializeObject<List<T>>(fileContent);
            }
        }

        private void SaveData()
        {
            lock (_lockData)
            {
                var fileContent = JsonConvert.SerializeObject(_data, Formatting.Indented);

                File.WriteAllText(_fileName, fileContent);
            }
        }
    }

    public class IdJsonFileRepository<T> : JsonFileRepository<T>, IIdJsonFileRepository<T> where T : IdEntity, new()
    {
        public IdJsonFileRepository(string fileName)
            : base(fileName)
        {
        }

        public override T Create()
        {
            var item = base.Create();

            item.Id = GetNextId();

            return item;
        }

        private int GetNextId()
        {
            LoadData();

            lock (_lockData)
            {
                return _data.Max(i => i.Id) + 1;
            }
        }

        public T GetById(int id)
        {
            return _data.Where(i => i.Id == id).SingleOrDefault();
        }
    }
}
