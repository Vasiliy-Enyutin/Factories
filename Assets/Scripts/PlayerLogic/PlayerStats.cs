using UnityEngine;

namespace PlayerLogic
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private int _inventorySize;
        [SerializeField] private float _grabSpeed;
        [SerializeField] private float _dropSpeed;


        public float MoveSpeed => _moveSpeed;

        public int InventorySize => _inventorySize;

        public float GrabSpeed => _grabSpeed;
        
        public float DropSpeed => _dropSpeed;
    }
}
