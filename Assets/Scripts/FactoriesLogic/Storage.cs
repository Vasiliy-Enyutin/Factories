using UnityEngine;

namespace FactoriesLogic
{
    public class Storage : MonoBehaviour
    {
        [SerializeField] private StorageType _storageType;
        [SerializeField] private Cell[] _cells;
        private int _resourcesNumber = 0;


        public ResourceTypes ResourceType { get; set; }
        
        public bool IsFull { get; private set; }
        
        public bool IsEmpty { get; private set; }

        public StorageType StorageType => _storageType;


        private void Awake()
        {
            CheckFullness();
        }

        public void AddResource(Resource resource, Cell cell)
        {
            cell.AddResource(resource);
            _resourcesNumber++;
            CheckFullness();
        }

        public Resource GetResource()
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                if (_cells[i].IsFull == true)
                {
                    _resourcesNumber--;
                    CheckFullness();
                    return _cells[i].GetResource();
                }
            }
            
            return null;
        }

        public Cell GetEmptyCell()
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                if (_cells[i].IsFull == true)
                {
                    continue;
                }
                return _cells[i];
            }

            return null;
        }

        private void CheckFullness()
        {
            if (_resourcesNumber <= 0)
                IsEmpty = true;
            else
                IsEmpty = false;

            if (_resourcesNumber >= _cells.Length)
                IsFull = true;
            else
                IsFull = false;
        }
    }
}
