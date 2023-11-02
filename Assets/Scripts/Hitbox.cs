using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField]
    protected bool m_isHitReceiver;
    [SerializeField]
    protected bool m_isHitGiver;
    [SerializeField]
    protected EAgentType m_agentType = EAgentType.Count;
    public CharacterControllerStateMachine ccsm;
    private void Start() {  }

    private void OnTriggerEnter(Collider other)
    {
        if(m_agentType == EAgentType.Enemy)
        {
            if(m_isHitReceiver && !m_isHitGiver)
            {
                if(other.gameObject.GetComponent<Hitbox>().m_agentType == EAgentType.Ally)
                ccsm.m_audioAndFX.PlayFXSys(EActionTYpe.Attack);
                Destroy(gameObject);
            }else if(m_isHitGiver)
            {
                ccsm.m_audioAndFX.PlayFXSys(EActionTYpe.Hit);
            }
        }
        /*
          chck if other is hitbox
         if other.agent = Ally
                kjjnkasf
        if other.agent = Enemy
                blablabla
        if other.agent = Neutral
                blablabla
         
         */
    }
}

public enum EAgentType
{
    Ally,
    Enemy,
    Neutral,
    Count//num isquecer, eh uma boua idéia!
}
