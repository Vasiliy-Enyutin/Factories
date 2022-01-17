using System.Collections;
using FactoriesLogic;
using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(PlayerStats))]
    [RequireComponent(typeof(PlayerInventory))]
    public class PlayerDropController : MonoBehaviour
    {
        [SerializeField] private Transform _newResourcePoint;
        private PlayerStats _playerStats;
        private PlayerInventory _playerInventory;
        private bool _isDropping = false;


        private void Awake()
        {
            _playerInventory = GetComponent<PlayerInventory>();
            _playerStats = GetComponent<PlayerStats>();
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Storage entranceStorage) && _isDropping == false 
                && entranceStorage.StorageType == StorageType.Entrance && entranceStorage.IsFull == false)
            {
                int droppingResourceIndex;
                Resource resource = _playerInventory.PopResourceByType(entranceStorage.ResourceType, out droppingResourceIndex);
                if (resource != null)
                {
                    StartCoroutine(DropRoutine(entranceStorage, resource));
                    MoveNewResourcePointDown(resource);
                    MoveInventoryResourcesDown(resource, droppingResourceIndex);
                }
            }
        }

        private IEnumerator DropRoutine(Storage entranceStorage, Resource resource)
        {
            _isDropping = true;
            Cell emptyCell = entranceStorage.GetEmptyCell();
            resource.RotateInDuration(_playerStats.DropDuration, emptyCell.transform);
            yield return resource.MoveInDuration(_playerStats.DropDuration, emptyCell.transform);
            resource.transform.SetParent(emptyCell.transform);
            entranceStorage.AddResource(resource, emptyCell);
            resource.GetComponent<BoxCollider>().enabled = false;
            _isDropping = false;
        }
        
        private void MoveInventoryResourcesDown(Resource resource, int droppingResourceIndex)
        {
            float resourceColliderY = resource.GetComponent<Collider>().bounds.size.y;
            for (int i = _playerInventory.Resources.Count - 1; i >= droppingResourceIndex; i--)
            {
                if (droppingResourceIndex < _playerStats.InventorySize - 1)
                {
                    _playerInventory.Resources[i].transform.position = GetNewDownPosition(i, resourceColliderY);
                }
            }
        }

        private Vector3 GetNewDownPosition(int i, float resourceColliderY)
        {
            return new Vector3(_playerInventory.Resources[i].transform.position.x,
                _playerInventory.Resources[i].transform.position.y - resourceColliderY,
                _playerInventory.Resources[i].transform.position.z);
        }
        
        private void MoveNewResourcePointDown(Resource resource)
        {
            float resourceColliderY = resource.GetComponent<BoxCollider>().bounds.size.y;
            _newResourcePoint.position -= new Vector3(0, resourceColliderY, 0);
        }
    }
}
