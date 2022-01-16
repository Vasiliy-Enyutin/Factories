using System.Collections;
using FactoriesLogic;
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
            if (other.TryGetComponent(out Resource newResource) && _isGrabbing == false && _playerInventory.IsFull == false)
            {
                StartCoroutine(GrabRoutine(newResource.transform));
            }
        }

        private IEnumerator GrabRoutine(Transform newResource)
        {
            _isGrabbing = true;
            MoveNewResourcePointUp(newResource);
            int elapsedFrames = 0;
            while (elapsedFrames != _playerStats.InterpolationFramesCount)
            {
                float interpolationRatio = (float)elapsedFrames / _playerStats.InterpolationFramesCount;
                newResource.position = Vector3.Lerp(newResource.position, _newResourcePoint.position, interpolationRatio);
                elapsedFrames = (elapsedFrames + 1) % (_playerStats.InterpolationFramesCount + 1);
                yield return null;
            }
            newResource.position = _newResourcePoint.position;
            newResource.rotation = transform.rotation;
            newResource.SetParent(transform);
            _playerInventory.AddResource(newResource);
            _isGrabbing = false;
            MoveNewResourcePointUp(newResource);
        }

        private void MoveNewResourcePointUp(Transform newResource)
        {
            float halfNewResourceColliderY = newResource.GetComponent<BoxCollider>().bounds.extents.y;
            _newResourcePoint.position += new Vector3(0f, halfNewResourceColliderY, 0f);
        }
    }
}
