using UnityEngine;

namespace FactoriesLogic
{
    public class Cell : MonoBehaviour
    {
        public bool IsFull { get; private set; }
        public Resource Resource { get; private set; }


        public void AddResource(Resource resource)
        {
            Resource = resource;
            IsFull = true;
        }

        public Resource GetResource()
        {
            IsFull = false;
            return Resource;
        }
    }
}
