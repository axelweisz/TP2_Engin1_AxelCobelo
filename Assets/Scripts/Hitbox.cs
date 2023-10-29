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


    private void OnTriggerEnter(Collider other)
    {
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
