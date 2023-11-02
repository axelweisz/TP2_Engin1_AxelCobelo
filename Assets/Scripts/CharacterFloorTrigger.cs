using UnityEngine;

public class CharacterFloorTrigger : MonoBehaviour
{
    public bool IsOnFloor { get; private set; }
    private CharacterControllerStateMachine ccsm;

    private void Start() { ccsm = GetComponentInParent<CharacterControllerStateMachine>();}

    private void OnTriggerStay(Collider other)
    {
        if (!IsOnFloor)
        {
            ccsm.m_audioAndFX.PlayFXSys(EActionTYpe.Land);
            Debug.Log("Vient de toucher le sol");
        }
        IsOnFloor = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Vient de quitter le sol");
        IsOnFloor = false;
    }
}
