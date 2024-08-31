using System;
using System.Collections.Generic;

namespace ArkheroClone.Infrastructure.StateMachine
{
    public abstract class BaseStateMachine
    {
        protected private Dictionary<Type, IExitableState> _states;
        protected private IExitableState _activeState = null;
        public void Enter<TState>() where TState : class, IState
        {
            _activeState?.Exit();
            IState state = GetState<TState>();
            _activeState = state;
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            _activeState?.Exit();
            IPayloadedState<TPayload> state = GetState<TState>();
            _activeState = state;
            state.Enter(payload);
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}