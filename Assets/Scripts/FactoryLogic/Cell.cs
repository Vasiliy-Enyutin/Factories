using ResourceLogic;
using UnityEngine;

namespace FactoryLogic
{
    public class Cell : MonoBehaviour
    {
        private Resource _resource;
        
        
        public bool IsFull { get; private set; }


        public void AddResource(Resource resource)
        {
            _resource = resource;
            IsFull = true;
        }

        public Resource GetResource()
        {
            IsFull = false;
            return _resource;
        }
    }
}
