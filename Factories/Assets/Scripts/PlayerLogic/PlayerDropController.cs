using System.Collections;
using System.Collections.Generic;
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
            if (other.TryGetComponent(out EntranceStorage storage) && _isDropping == false && storage.IsFull == false)
            {
                int droppingResourceIndex;
                Transform resource = _playerInventory.PopResourceByType(storage.ResourceType, out droppingResourceIndex);
                if (resource != null)
                {
                    StartCoroutine(DropRoutine(storage, resource));
                    MoveInventoryResourcesDown(resource, droppingResourceIndex);
                }
            }
        }

        private IEnumerator DropRoutine(EntranceStorage storage, Transform resource)
        {
            _isDropping = true;
            float resourceColliderY = resource.GetComponent<Collider>().bounds.size.y;
            Transform cellToMove = storage.GetCell();
            int elapsedFrames = 0;
            while (elapsedFrames != _playerStats.InterpolationFramesCount)
            {
                float interpolationRatio = (float)elapsedFrames / _playerStats.InterpolationFramesCount;
                resource.position = Vector3.Lerp(resource.position, cellToMove.position, interpolationRatio);
                elapsedFrames = (elapsedFrames + 1) % (_playerStats.InterpolationFramesCount + 1);
                yield return null;
            }
            resource.position = cellToMove.position;
            resource.rotation = cellToMove.rotation;
            resource.SetParent(cellToMove);
            storage.AddResource(resource);
            MoveNewResourcePointDown(resource);
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
