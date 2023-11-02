using UnityEngine;
using System.Collections.Generic;

public class GameManagerSM : BaseStateMachine<IState>
{
    //public AudioAndFXManager m_audioAndFX;
    [SerializeField]
    protected Camera m_gameplayCamera;
    [SerializeField]
    protected Camera m_cinematicCamera;

    private static GameManagerSM _instance;

    public static GameManagerSM Instance
    {
        get { return _instance; }
    }

    protected override void CreatePossibleStates()
    {
        m_possibleStates = new List<IState>();
        m_possibleStates.Add(new GameplayState(m_gameplayCamera));
        m_possibleStates.Add(new CinematicState(m_cinematicCamera));
    }


    protected override void Start()
    {
        // Ensure there's only one instance of the GameManagerSM
        if (_instance == null)
        {
            _instance = this;
            foreach (IState state in m_possibleStates)
            {
                state.OnStart();
            }
            m_currentState = m_possibleStates[0];
            m_currentState.OnEnter();
        }
        else
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
        }
    }
}