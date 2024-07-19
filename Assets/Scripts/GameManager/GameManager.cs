using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZUNGAS.Core.Singleton;
using ZUNGAS.StateMachine;

public class GameManager : Singleton<GameManager>
{
    public enum GameStates
    {
        INTRO,
        WALKING,
        IDLE,
        JUMP,
        LOSE
    }

    public StateMachine<GameStates> stateMachine;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        stateMachine = new StateMachine<GameStates>();

        stateMachine.Init();
        stateMachine.RegisterStates(GameStates.INTRO, new GMStateIntro());
        stateMachine.RegisterStates(GameStates.WALKING, new StateBase());
        stateMachine.RegisterStates(GameStates.IDLE, new StateBase());
        stateMachine.RegisterStates(GameStates.JUMP, new StateBase());
        stateMachine.RegisterStates(GameStates.LOSE, new StateBase());

        stateMachine.SwitchState(GameStates.INTRO);
    }

}
