using UnityEngine;

namespace PopupTextLogic
{
    public class FactoryPopupText : MonoBehaviour
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
