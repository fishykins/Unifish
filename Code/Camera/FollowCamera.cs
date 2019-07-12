using UnityEngine;

namespace Unifish
{
    public class FollowCamera : MonoBehaviour
    {
        public Transform cameraTarget;
        public float smoothSpeed = 10f;
        public Vector3 offset;
        public float minHeight = 5f;

        public enum behindAxis { x,y,z};
        public behindAxis followAxis;

        void LateUpdate()
        {
            Vector3 targetPosition = Vector3.zero;

            switch (followAxis) {
                case behindAxis.x:
                    targetPosition = cameraTarget.position + cameraTarget.right * offset.z;
                    targetPosition.y += offset.y;
                    targetPosition.x += offset.x;
                    break;
                case behindAxis.y:
                    targetPosition = cameraTarget.position + cameraTarget.forward * offset.z;
                    targetPosition.y += offset.y;
                    targetPosition.x += offset.x;
                    break;
                case behindAxis.z:
                    targetPosition = cameraTarget.position + cameraTarget.up * offset.z;
                    targetPosition.y += offset.y;
                    targetPosition.x += offset.x;
                    break;
            }

            RaycastHit hit;
            Color rayCol = Color.red;
            if (UnityEngine.Physics.Raycast(transform.position, -transform.up, out hit, minHeight)) {
                targetPosition.y = hit.point.y + minHeight;
            }

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
            transform.LookAt(cameraTarget);
        }
    }
}