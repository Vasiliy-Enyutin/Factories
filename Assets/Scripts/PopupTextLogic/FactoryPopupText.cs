using System;
using UnityEngine;

namespace PopupTextLogic
{
    [RequireComponent(typeof(Animator))]
    public class FactoryPopupText : MonoBehaviour
    {
        private Animator _animator;
        
        public event Action<FactoryPopupText> OnPopupTextDestroy;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnDestroy()
        {
            OnPopupTextDestroy?.Invoke(this);
        }

        public void Destroy()
        {
            _animator.Play("PopupText_Close");
        }

        private void Die()  // called from animator
        {
            Destroy(gameObject);
        }
    }
}
