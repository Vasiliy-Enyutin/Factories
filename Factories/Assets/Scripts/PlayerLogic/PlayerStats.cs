using UnityEngine;

namespace PlayerLogic
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;


        public float MoveSpeed => _moveSpeed;
    }
}
