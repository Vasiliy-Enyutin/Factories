using System.Collections;
using FactoryLogic;
using ResourceLogic;
using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(PlayerInventory))]
    [RequireComponent(typeof(PlayerStats))]
    public class PlayerGrabController : MonoBehaviour
    {
        [SerializeField] private Transform _newResourcePoint;
        private PlayerStats _playerStats;
        private PlayerInventory _playerInventory;
        private bool _isGrabbing = false;


        private void Awake()
        {
            _playerInventory = GetComponent<PlayerInventory>();
            _playerStats = GetComponent<PlayerStats>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Storage exitStorage) && exitStorage.StorageType == StorageType.Exit 
                && exitStorage.IsEmpty == false && _isGrabbing == false && _playerInventory.IsFull == false)
            {
                Resource newResource = exitStorage.GetResource();
                if (newResource != null)
                {
                    StartCoroutine(GrabRoutine(newResource));
                }
            }
        }
        
        private IEnumerator GrabRoutine(Resource newResource)
        {
            _isGrabbing = true;
            MoveNewResourcePointUp(newResource);
            newResource.GetComponent<Resource>().RotateInDuration(_playerStats.GrabDuration, _newResourcePoint);
            yield return newResource.GetComponent<Resource>().MoveInDuration(_playerStats.GrabDuration, _newResourcePoint);
            newResource.transform.SetParent(transform);
            _playerInventory.AddResource(newResource);
            _isGrabbing = false;
            MoveNewResourcePointUp(newResource);
        }

        private void MoveNewResourcePointUp(Resource newResource)
        {
            float halfNewResourceColliderY = newResource.GetComponent<BoxCollider>().bounds.extents.y;
            _newResourcePoint.position += new Vector3(0f, halfNewResourceColliderY, 0f);
        }
    }
}
