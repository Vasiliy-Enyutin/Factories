using UnityEngine;

namespace PlayerLogic
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private int _inventorySize;
        [SerializeField] private int _interpolationFramesCount;


        public float MoveSpeed => _moveSpeed;

        public int InventorySize => _inventorySize;

        public int InterpolationFramesCount => _interpolationFramesCount;
    }
}
