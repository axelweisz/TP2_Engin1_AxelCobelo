using UnityEngine;

public class NonPlayingState : CharacterState
{
    public override bool CanEnter(IState currentState){ return false;}

    public override bool CanExit() { return true;}

    public override void OnEnter(){ Debug.Log("On Enter NonPlayingState"); }

    public override void OnExit() { Debug.Log("On Exit NonPlayingState"); }

    public override void OnFixedUpdate() { }

}
