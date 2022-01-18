using System.Collections.Generic;
using FactoryLogic;
using UnityEngine;

namespace PopupTextLogic
{
    public class FactoryPopupTextController : MonoBehaviour
    {
        [SerializeField] private FactoryPopupText _entranceStorageIsEmptyPopupTextPrefab;
        [SerializeField] private FactoryPopupText _exitStorageIsFullPopupTextPrefab;
        
        [SerializeField] private Vector3 _entranceStorageIsEmptyMessageOffset;
        [SerializeField] private Vector3 _exitStorageIsFullMessageOffset;

        private Factory[] _factories;
        private readonly Dictionary<FactoryPopupText, Factory> _popups = new Dictionary<FactoryPopupText, Factory>();

        private void OnDisable()
        {
            foreach (Factory factory in _factories)
            {
                factory.OnEntranceStorageEmpty -= InstantiateEntranceStorageEmptyPopupText;
                factory.OnExitStorageFull -= InstantiateExitStorageFullPopupText;
            }
        }

        private void Start()
        {
            _factories = FindObjectsOfType<Factory>();

            foreach (Factory factory in _factories)
            {
                factory.OnEntranceStorageEmpty += InstantiateEntranceStorageEmptyPopupText;
                factory.OnExitStorageFull += InstantiateExitStorageFullPopupText;
            }
        }

        private void InstantiateEntranceStorageEmptyPopupText(Factory factory)
        {
            FactoryPopupText newPopup = Instantiate(_entranceStorageIsEmptyPopupTextPrefab,
                factory.transform.position + _entranceStorageIsEmptyMessageOffset, Quaternion.identity);
            factory.OnEntranceStorageNotEmpty += newPopup.Destroy;
            
            RegisterPopup(factory, newPopup);
        }

        private void InstantiateExitStorageFullPopupText(Factory factory)
        {
            FactoryPopupText newPopup = Instantiate(_exitStorageIsFullPopupTextPrefab,
                factory.transform.position + _exitStorageIsFullMessageOffset, Quaternion.identity);
            factory.OnExitStorageNotFull += newPopup.Destroy;
            
            RegisterPopup(factory, newPopup);
        }

        private void RegisterPopup(Factory factory, FactoryPopupText popup)
        {
            popup.OnPopupTextDestroy += UnregisterPopup;
            _popups.Add(popup, factory);
        }

        private void UnregisterPopup(FactoryPopupText popup)
        {
            if (_popups.TryGetValue(popup, out Factory factory))
            {
                factory.OnEntranceStorageNotEmpty -= popup.Destroy;
                factory.OnExitStorageNotFull -= popup.Destroy;

                _popups.Remove(popup);
            }
        }
    }
}
