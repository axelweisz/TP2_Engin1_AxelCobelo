using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float sensitivity = 0.02f;
    CinemachineTransposer transposer;
    private Vector3 lastMousePosition;
    [SerializeField]
    private float minOffsetX = -3.0f;
    [SerializeField]
    private float maxOffsetX = 3.0f;
    [SerializeField]
    private float minOffsetY = 0.0f;
    [SerializeField]
    private float maxOffsetY = 5.0f;
    [SerializeField]
    private float minOffsetZ = -3.5f;
    [SerializeField]
    private float maxOffsetZ = -3.5f;
    private void Start()
    {
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        UpdateCamOffsets();
    }

    //#region updates/mov
    //private void FixedUpdate()//do all physics here
    //{
    //    FixedUpdateCameraLerp();
    //    MoveCameraInFrontOfObstructionsFUpdate();
    //}

    //private void FixedUpdateCameraLerp()
    //{
    //    var desiredPosition = m_objectToLookAt.position - (transform.forward * m_desiredDistance);
    //    transform.position = Vector3.Lerp(transform.position, desiredPosition, m_lerpSpeed);
    //}

    private void UpdateCamOffsets()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        if (currentMousePosition != lastMousePosition)
        {
            //je ne sais pas mais le code fonctionne juste pour le axis horizontal
            float mouseX = (currentMousePosition.x - lastMousePosition.x) * sensitivity;
            //float mouseY = (currentMousePosition.y - lastMousePosition.y) * sensitivity;
           // float scrollInput = Input.GetAxis("Mouse ScrollWheel") * sensitivity/1000;

            // Update X and Y offsets
            Vector3 newFollowOffset = transposer.m_FollowOffset + new Vector3(mouseX, 0f, 0f);
            newFollowOffset.x = Mathf.Clamp(newFollowOffset.x, minOffsetX, maxOffsetX);
            //newFollowOffset.y = Mathf.Clamp(newFollowOffset.y, minOffsetY, minOffsetY);

            // Apply the clamped offsets
            transposer.m_FollowOffset.x = newFollowOffset.x;
            //transposer.m_FollowOffset.y = newFollowOffset.y;


            // Update Z offset
            //float newZOffset = transposer.m_FollowOffset.z + scrollInput;
            //newZOffset = Mathf.Clamp(newZOffset, minOffsetZ, minOffsetZ);
           // transposer.m_FollowOffset.z = newZOffset;

            lastMousePosition = currentMousePosition;
        }
    }
  
}
