using System.Collections;
using UnityEngine;

namespace ResourceLogic
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] private ResourceType resourceType;


        public ResourceType ResourceType => resourceType;

        public Coroutine MoveInDuration(float duration, Transform moveTo)
        {
            return StartCoroutine(MoveResourceRoutine(duration, moveTo));
        }
        
        public Coroutine RotateInDuration(float duration, Transform rotateTo)
        {
            return StartCoroutine(RotateResourceRoutine(duration, rotateTo));
        }

        private IEnumerator MoveResourceRoutine(float duration, Transform objectToMove)
        {
            float time = 0f;
            Vector3 startPosition = transform.position;
            while (time <= duration)
            {
                transform.position = Vector3.Lerp(startPosition, objectToMove.position, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            transform.position = objectToMove.position;
        }
        
        private IEnumerator RotateResourceRoutine(float duration, Transform rotateTo)
        {
            float time = 0f;
            Quaternion startRotation = transform.rotation;
            while (time <= duration)
            {
                transform.rotation = Quaternion.Lerp(startRotation, rotateTo.rotation, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            transform.rotation = rotateTo.rotation;
        }
    }
}
