using UnityEngine;

namespace FactoriesLogic
{
    public class Cell : MonoBehaviour
    {
        public bool IsFull { get; private set; }
        public Transform Resource { get; private set; }


        public void AddResource(Transform resource)
        {
            Resource = resource;
            IsFull = true;
        }

        public Transform GetResource()
        {
            IsFull = false;
            return Resource;
        }
    }
}
