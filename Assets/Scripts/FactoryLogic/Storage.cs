using System;
using ResourceLogic;
using UnityEngine;

namespace FactoryLogic
{
    public class Storage : MonoBehaviour
    {
        [SerializeField] private StorageType _storageType;
        [SerializeField] private Cell[] _cells;
        private int _resourcesNumber = 0;

        public event Action OnStorageChanged;


        public ResourceType ResourceType { get; set; }
        
        public StorageType StorageType => _storageType;
        
        public bool IsFull { get; private set; }
        
        public bool IsEmpty { get; private set; }


        private void Start()
        {
            CheckOccupancy();
        }


        public void AddResource(Resource resource, Cell cell)
        {
            cell.AddResource(resource);
            _resourcesNumber++;
            CheckOccupancy();
        }

        public Resource GetResource()
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                if (_cells[i].IsFull == true)
                {
                    _resourcesNumber--;
                    CheckOccupancy();
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

        private void CheckOccupancy()
        {
            if (_resourcesNumber <= 0)
                IsEmpty = true;
            else
                IsEmpty = false;
            
            if (_resourcesNumber >= _cells.Length)
                IsFull = true;
            else
                IsFull = false;

            OnStorageChanged?.Invoke();
        }
    }
}
