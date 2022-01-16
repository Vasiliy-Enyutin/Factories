using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FactoriesLogic
{
    public class EntranceStorage : MonoBehaviour
    {
        [SerializeField] private ResourceTypes _resourceType;
        [SerializeField] private Transform[] _cells;
        private Stack<Transform> _resources;
        private bool _isFull = false;


        public ResourceTypes ResourceType { get; private set; }

        public bool IsFull => _isFull;

        private void Awake()
        {
            ResourceType = _resourceType;
            _resources = new Stack<Transform>();
        }

        public Transform GetCell()
        {
            return _cells[_resources.Count];
        }
        
        public void AddResource(Transform resource)
        {
            _resources.Push(resource);
            CheckFullness();
        }

        private void CheckFullness()
        {
            if (_resources.Count >= _cells.Length)
                _isFull = true;
            else
                _isFull = false;
        }
    }
}
