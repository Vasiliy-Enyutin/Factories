using System.Collections.Generic;
using FactoriesLogic;
using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(PlayerStats))]
    public class PlayerInventory : MonoBehaviour
    {
        private List<Transform> _resources;
        private PlayerStats _playerStats;

        
        public bool IsFull { get; private set; }

        public List<Transform> Resources => _resources;
        

        private void Awake()
        {
            _playerStats = GetComponent<PlayerStats>();
            _resources = new List<Transform>(_playerStats.InventorySize);
        }

        public void AddResource(Transform resource)
        {
            _resources.Add(resource);
            CheckFullness();
        }

        public Transform PopResourceByType(ResourceTypes type, out int resourceIndex)
        {
            Transform resource;
            for (int i = _resources.Count - 1; i >= 0; i--)
            {
                if (_resources[i].GetComponent<Resource>().ResourceType == type)
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
