using UnityEngine;

namespace FactoriesLogic
{
    public abstract class StorageBase : MonoBehaviour
    {
        [SerializeField] protected Cell[] _cells;
        protected int _resourcesNumber = 0;


        public bool IsFull { get; private set; }
        
        public bool IsEmpty { get; private set; }


        protected virtual void Awake()
        {
            CheckFullness();
        }

        public void AddResource(Transform resource, Cell cell)
        {
            cell.AddResource(resource);
            _resourcesNumber++;
            CheckFullness();
        }

        public Transform GetResource()
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
