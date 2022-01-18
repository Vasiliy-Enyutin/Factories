using FactoryLogic;
using UnityEngine;

namespace PopupTextLogic
{
    public class FactoryPopupTextController : MonoBehaviour
    {
        [SerializeField] private Transform _entranceStorageIsEmptyPopupTextPrefab;
        [SerializeField] private Transform _exitStorageIsFullPopupTextPrefab;
        [SerializeField] private Vector3 _entranceStorageIsEmptyMessageOffset;
        [SerializeField] private Vector3 _exitStorageIsFullMessageOffset;
        
        
        private void OnEnable()
        {
            FactoryStorageOccupancyEventBrocker.OnEntranceStorageEmpty += InstantiateEntranceStorageEmptyPopupText;
            FactoryStorageOccupancyEventBrocker.OnEntranceStorageNotEmpty += DeleteEntranceStorageEmptyPopupText;
            FactoryStorageOccupancyEventBrocker.OnExitStorageFull += InstantiateExitStorageFullPopupText;
            FactoryStorageOccupancyEventBrocker.OnExitStorageNotFull += DeleteExitStorageFullPopupText;
        }

        private void OnDisable()
        {
            FactoryStorageOccupancyEventBrocker.OnEntranceStorageEmpty -= InstantiateEntranceStorageEmptyPopupText;
            FactoryStorageOccupancyEventBrocker.OnEntranceStorageNotEmpty -= DeleteEntranceStorageEmptyPopupText;
            FactoryStorageOccupancyEventBrocker.OnExitStorageFull -= InstantiateExitStorageFullPopupText;
            FactoryStorageOccupancyEventBrocker.OnExitStorageNotFull -= DeleteExitStorageFullPopupText;
        }

        private void InstantiateEntranceStorageEmptyPopupText(Factory factory)
        {
            factory.EntranceStorateEmptyPopupText = Instantiate(_entranceStorageIsEmptyPopupTextPrefab,
                factory.transform.position + _entranceStorageIsEmptyMessageOffset, Quaternion.identity);
        }

        private void DeleteEntranceStorageEmptyPopupText(Factory factory)
        {
            if (factory.EntranceStorateEmptyPopupText == null)
                return;
            
            factory.EntranceStorateEmptyPopupText.GetComponent<FactoryPopupText>().Destroy();
        }
        
        private void InstantiateExitStorageFullPopupText(Factory factory)
        {
            factory.ExitStorageFullPopupText = Instantiate(_exitStorageIsFullPopupTextPrefab,
                factory.transform.position + _exitStorageIsFullMessageOffset, Quaternion.identity);
        }

        private void DeleteExitStorageFullPopupText(Factory factory)
        {
            if (factory.ExitStorageFullPopupText == null)
                return;
            
            factory.ExitStorageFullPopupText.GetComponent<FactoryPopupText>().Destroy();
        }
    }
}
