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
        private const float FinalProgress = 1;
        
        
        private void Awake()
        {
            _playerInventory = GetComponent<PlayerInventory>();
            _playerStats = GetComponent<PlayerStats>();
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out EntranceStorage storage) && _isDropping == false && storage.IsFull == false)
            {
                int droppingResourceIndex;
                Transform resource = _playerInventory.PopResourceByType(storage.ResourceType, out droppingResourceIndex);
                if (resource != null)
                {
                    StartCoroutine(DropRoutine(storage, resource));
                    MoveNewResourcePointDown(resource);
                    MoveInventoryResourcesDown(resource, droppingResourceIndex);
                }
            }
        }

        private IEnumerator DropRoutine(EntranceStorage storage, Transform resource)
        {
            _isDropping = true;
            Cell emptyCell = storage.GetEmptyCell();
            float progress = 0;
            while (progress <= FinalProgress)
            {
                resource.position = Vector3.Lerp(resource.position, emptyCell.transform.position, progress);
                progress += Time.deltaTime * _playerStats.DropSpeed;
                yield return null;
            }
            resource.position = emptyCell.transform.position;
            resource.rotation = emptyCell.transform.rotation;
            resource.SetParent(emptyCell.transform);
            storage.AddResource(resource, emptyCell);
            resource.GetComponent<BoxCollider>().enabled = false;
            _isDropping = false;
        }
        
        private void MoveInventoryResourcesDown(Transform resource, int droppingResourceIndex)
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
        
        private void MoveNewResourcePointDown(Transform resource)
        {
            float resourceColliderY = resource.GetComponent<BoxCollider>().bounds.size.y;
            _newResourcePoint.position -= new Vector3(0, resourceColliderY, 0);
        }
    }
}
