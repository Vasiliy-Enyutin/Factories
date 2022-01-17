using UnityEngine;

namespace FactoriesLogic
{
    public class EntranceStorage : StorageBase
    {
        [SerializeField] private ResourceTypes _resourceType;


        public ResourceTypes ResourceType { get; private set; }


        protected override void Awake()
        {
            base.Awake();
            ResourceType = _resourceType;
        }
    }
}
