using System.Collections.Generic;
using FactoriesLogic;
using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(PlayerStats))]
    public class PlayerInventory : MonoBehaviour
    {
        private List<Resource> _resources;
        private PlayerStats _playerStats;

        
        public bool IsFull { get; private set; }

        public List<Resource> Resources => _resources;
        

        private void Awake()
        {
            _playerStats = GetComponent<PlayerStats>();
            _resources = new List<Resource>(_playerStats.InventorySize);
        }

        public void AddResource(Resource resource)
        {
            _resources.Add(resource);
            CheckFullness();
        }

        public Resource PopResourceByType(ResourceTypes type, out int resourceIndex)
        {
            Resource resource;
            for (int i = _resources.Count - 1; i >= 0; i--)
            {
                if (_resources[i].ResourceType == type)
                {
                    CheckFullness();
                    resourceIndex = i;
                    resource = _resources[i];
                    _resources.RemoveAt(i);
                    return resource;
                }
            }

            resourceIndex = -1;
            return null;
        }
        
        private void CheckFullness()
        {
            if (_resources.Count >= _playerStats.InventorySize)
                IsFull = true;
            else
                IsFull = false;
        }
    }
}
