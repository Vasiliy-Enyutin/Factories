using System.Collections.Generic;
using ResourceLogic;
using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(PlayerStats))]
    public class PlayerInventory : MonoBehaviour
    {
        private PlayerStats _playerStats;

        
        public bool IsFull { get; private set; }

        public List<Resource> Resources { get; private set; }


        private void Awake()
        {
            _playerStats = GetComponent<PlayerStats>();
            Resources = new List<Resource>(_playerStats.InventorySize);
        }

        public void AddResource(Resource resource)
        {
            Resources.Add(resource);
            CheckFullness();
        }

        public Resource PopResourceByType(ResourceType type, out int resourceIndex)
        {
            Resource resource;
            for (int i = Resources.Count - 1; i >= 0; i--)
            {
                if (Resources[i].ResourceType == type)
                {
                    CheckFullness();
                    resourceIndex = i;
                    resource = Resources[i];
                    Resources.RemoveAt(i);
                    return resource;
                }
            }

            resourceIndex = -1;
            return null;
        }
        
        private void CheckFullness()
        {
            if (Resources.Count >= _playerStats.InventorySize)
                IsFull = true;
            else
                IsFull = false;
        }
    }
}
