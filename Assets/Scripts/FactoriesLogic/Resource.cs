using UnityEngine;

namespace FactoriesLogic
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] private ResourceTypes _resourceTypes;


        public ResourceTypes ResourceType => _resourceTypes;
    }
}
