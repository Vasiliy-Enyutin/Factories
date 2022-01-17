using UnityEngine;

namespace PlayerLogic
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private int _inventorySize;
        [SerializeField] private float _grabDuration;
        [SerializeField] private float _dropDuration;


        public float MoveSpeed => _moveSpeed;

        public int InventorySize => _inventorySize;

        public float GrabDuration => _grabDuration;
        
        public float DropDuration => _dropDuration;
    }
}
