﻿using _src.CodeBase.Infrastructure.States;
using _src.CodeBase.Logic;
using _src.CodeBase.Services.Input;
using UnityEngine;

namespace _src.CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;


        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain);
        }
    }
}