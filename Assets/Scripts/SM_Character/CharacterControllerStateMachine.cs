using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerStateMachine : BaseStateMachine<CharacterState>, IDamageable
{
    public AudioAndFXManager m_audioAndFX;
    public Camera Camera { get; private set; }
    [field:SerializeField]
    public Rigidbody RB { get; private set; }
    [field:SerializeField]
    public Animator Animator { get; private set; }

    [field: SerializeField]
    public float InAirAccelerationValue { get; private set; } = 0.2f;
    [field: SerializeField]
    public float AccelerationValue { get; private set; }
    [field: SerializeField]
    public float DecelerationValue { get; private set; } = 0.3f;
    [field: SerializeField]
    public float MaxForwardVelocity { get; private set; }
    [field: SerializeField]
    public float MaxSidewaysVelocity { get; private set; }
    [field: SerializeField]
    public float MaxBackwardVelocity { get; private set; }
    private Vector2 CurrentRelativeVelocity { get; set; }
    public Vector2 CurrentDirectionalInputs { get; private set; }
    public bool OnHitStimuliReceived { get; set; } = false;
    public bool OnStunStimuliReceived { get; set; } = false;

    [field: SerializeField]
    public float JumpIntensity { get; private set; } = 1000.0f;

    [SerializeField]
    private CharacterFloorTrigger m_floorTrigger;

    [SerializeField]
    public GameObject fistCollider;

    protected override void CreatePossibleStates() 
    {
        m_possibleStates = new List<CharacterState>();
        m_possibleStates.Add(new FreeState());
        m_possibleStates.Add(new JumpState());
        m_possibleStates.Add(new FallingState());
        m_possibleStates.Add(new HitState());
        m_possibleStates.Add(new GroundState());
        m_possibleStates.Add(new AttackingState());
    }

    protected override void Start()
    {
        foreach (CharacterState state in m_possibleStates)
        {
            state.OnStart(this);
        }
        //TO DO: fazer aqui o if(GM.CinematicSM)
        m_currentState = m_possibleStates[0];
        m_currentState.OnEnter();

        Camera = Camera.main;
    }

    protected override void Update()
    {
        base.Update();
        UpdateAnimatorValues();
        //GetKeyDowns();
    }

    protected override void FixedUpdate()
    {
        SetDirectionalInputs();
        base.FixedUpdate();
        Set2dRelativeVelocity();
    }

    public bool IsInContactWithFloor() { return m_floorTrigger.IsOnFloor; }

    private void UpdateAnimatorValues()
    {
        //Aller chercher ma vitesse actuelle
        //Communiquer directement avec mon Animator
        //pq a gente divide pela MaxSpeed mesmo>?
        Animator.SetFloat("MoveX", CurrentRelativeVelocity.x / GetCurrentMaxSpeed());
        Animator.SetFloat("MoveY", CurrentRelativeVelocity.y / GetCurrentMaxSpeed());
        Animator.SetBool("TouchGround", m_floorTrigger.IsOnFloor);
    }

    private void Set2dRelativeVelocity()
    {
        //RB transform world2local -> get velocity relative 2 RB orientation.
        Vector3 relativeVelocity = RB.transform.InverseTransformDirection(RB.velocity);
        CurrentRelativeVelocity = new Vector2(relativeVelocity.x, relativeVelocity.z);
    }

    public float GetCurrentMaxSpeed()
    {
        //chercher la composante réelle de nos vitesses 
        //On prend notre vitesse en z (l'avant/arrière) et en x (côtés)  
        //notre vitesse actuelle réelle dépendra du résultat

        //se chega assez perto de 0 (dos lados?) , retorna a Max Veloci// p/ frente
        if (Mathf.Approximately(CurrentDirectionalInputs.magnitude, 0))  { return MaxForwardVelocity;}

        //aqui pra baixo calcula a MaxVeloc nas diagonais?
        var normalizedInputs = CurrentDirectionalInputs.normalized;

        //pq eleva ao quadrado os inputs?
        var currentMaxVelocity = Mathf.Pow(normalizedInputs.x, 2) * MaxSidewaysVelocity;

        if (normalizedInputs.y > 0)
        {
            currentMaxVelocity += Mathf.Pow(normalizedInputs.y, 2) * MaxForwardVelocity;
        }
        else
        {
            currentMaxVelocity += Mathf.Pow(normalizedInputs.y, 2) * MaxBackwardVelocity;
        }

        return currentMaxVelocity;
    }

    //seta os CurrDirInputs usados no GetCurrMaxSpeed
    //usados tb no fixedUpdate do Free State
    public void SetDirectionalInputs()
    {
        CurrentDirectionalInputs = Vector2.zero;

        //Pourquoi est-ce que cette méthode s'appelle Get même si elle n'a pas de valeur de retour?
        // Ce n'est pas une erreur!
        if (Input.GetKey(KeyCode.W))
        {
            CurrentDirectionalInputs += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            CurrentDirectionalInputs += Vector2.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            CurrentDirectionalInputs += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            CurrentDirectionalInputs += Vector2.right;
        }
    }

    //receive damage tem que fazer nu inimigu tb... (e eke tem que ser DAmageable tb)
    public void ReceiveDamage(EDamageType damageType)
    {
        if (damageType == EDamageType.Normal)
            OnHitStimuliReceived = true;
        
        if (damageType == EDamageType.Stunning)
            OnStunStimuliReceived = true;
    }
    //essa aqui tem que ver qdo usa (Attack, Ground e Hit States)
    public void FixedUpdateQuickDeceleration()
    {
        var oppositeDirectionForceToApply = -RB.velocity * DecelerationValue * Time.fixedDeltaTime;
        RB.AddForce(oppositeDirectionForceToApply, ForceMode.Acceleration);
    }
    //function called with animation events
    public void TogglePunchHitBox(bool activate)
    {
        fistCollider.SetActive(activate);
    }
}
