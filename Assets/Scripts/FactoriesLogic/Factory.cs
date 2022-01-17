using System.Collections;
using UnityEngine;

namespace FactoriesLogic
{
    public class Factory : MonoBehaviour
    {
        [SerializeField] private float _generationTime;
        [SerializeField] private float _generationSpeed;
        [SerializeField] private ExitStorage _exitStorage;
        [SerializeField] private EntranceStorage _entranceStorage;
        [SerializeField] private GameObject _exitResource;
        private bool _isGenerating = false;
        private const int FinalProgress = 1;

        private void Update()
        {
            if (_entranceStorage != null && _entranceStorage.IsEmpty == true)
                return;
            
            if (_isGenerating == true || _exitStorage.IsFull == true)
                return;
            
            StartCoroutine(GenerateRoutine());
        }

        private IEnumerator GenerateRoutine()
        {
            _isGenerating = true;
            if (_entranceStorage != null)
            {
                Transform entranceResource = _entranceStorage.GetResource();
                yield return StartCoroutine(MoveResourceRoutine(entranceResource, transform));
                Destroy(entranceResource.gameObject);
            }

            yield return StartCoroutine(GenerateTimer());
            GameObject resource = Instantiate(_exitResource, transform.position, Quaternion.identity);
            Cell emptyCell = _exitStorage.GetEmptyCell();
            yield return StartCoroutine(MoveResourceRoutine(resource.transform, emptyCell.transform));
            _exitStorage.AddResource(resource.transform, emptyCell);
            _isGenerating = false;
        }

        private IEnumerator MoveResourceRoutine(Transform resource, Transform objectToMove)
        {
            float progress = 0f;
            while (progress <= FinalProgress)
            {
                resource.position = Vector3.Lerp(resource.position, objectToMove.position, progress);
                progress += Time.deltaTime * _generationSpeed;
                yield return null;
            }
            resource.position = objectToMove.position;
            resource.rotation = objectToMove.rotation;
        }
        
        private IEnumerator GenerateTimer()
        {
            yield return new WaitForSeconds(_generationTime);
        }
    }
}
