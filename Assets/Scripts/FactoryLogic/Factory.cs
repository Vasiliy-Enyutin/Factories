using System.Collections;
using ResourceLogic;
using UnityEngine;

namespace FactoryLogic
{
    public class Factory : MonoBehaviour
    {
        [SerializeField] private ResourceType _entranceResourceType;
        [SerializeField] private ResourceType _exitResourceType;
        [SerializeField] private float _generationTime;
        [SerializeField] private float _resourceMoveDuration;
        [SerializeField] private Storage _entranceStorage;
        [SerializeField] private Storage _exitStorage;
        [SerializeField] private Resource _exitResource;
        private bool _isGenerating = false;


        public Transform EntranceStorateEmptyPopupText { get; set; }
        
        public Transform ExitStorageFullPopupText { get; set; }


        private void Awake()
        {
            if (_entranceStorage != null)
                _entranceStorage.ResourceType = _entranceResourceType;
            
            _exitStorage.ResourceType = _exitResourceType;
        }
        
        private void OnEnable()
        {
            if (_entranceStorage != null)
                _entranceStorage.OnStorageChanged += ReportEntranceStorageOccupancy;
            
            _exitStorage.OnStorageChanged += ReportExitStorageOccupancy;
        }

        private void OnDisable()
        {
            if (_entranceStorage != null)
                _entranceStorage.OnStorageChanged -= ReportEntranceStorageOccupancy;
            
            _exitStorage.OnStorageChanged -= ReportExitStorageOccupancy;
        }

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
                Resource entranceResource = _entranceStorage.GetResource();
                yield return entranceResource.MoveInDuration(_resourceMoveDuration, transform);
                Destroy(entranceResource.gameObject);
            }

            yield return StartCoroutine(GenerateTimer());
            Resource resource = Instantiate(_exitResource, transform.position, Quaternion.identity);
            Cell emptyCell = _exitStorage.GetEmptyCell();
            resource.RotateInDuration(_resourceMoveDuration, emptyCell.transform);
            yield return resource.MoveInDuration(_resourceMoveDuration, emptyCell.transform);
            _exitStorage.AddResource(resource, emptyCell);
            _isGenerating = false;
        }

        private IEnumerator GenerateTimer()
        {
            yield return new WaitForSeconds(_generationTime);
        }

        private void ReportEntranceStorageOccupancy()
        {
            if (_entranceStorage.IsEmpty == true)
                FactoryStorageOccupancyEventBrocker.InvokeEntranceStorageEmpty(this);
            else
                FactoryStorageOccupancyEventBrocker.InvokeEntranceStorageNotEmpty(this);
        }
        
        private void ReportExitStorageOccupancy()
        {
            if (_exitStorage.IsFull == true)
                FactoryStorageOccupancyEventBrocker.InvokeExitStorageFull(this);
            else
                FactoryStorageOccupancyEventBrocker.InvokeExitStorageNotFull(this);
        }
    }
}
