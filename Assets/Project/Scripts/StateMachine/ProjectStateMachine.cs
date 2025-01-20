using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.StateMachine.SpecificStates;
using UnityEngine;
using VContainer.Unity;

namespace Project.Scripts.StateMachine
{
    public class ProjectStateMachine : IPostInitializable, ITickable, IDisposable
    {
        private readonly StatesFactory _statesFactory;
        private readonly CancellationTokenSource _cancellationTokenSource;

        private IState _activeState;

        public ProjectStateMachine(StatesFactory statesFactory)
        {
            _statesFactory = statesFactory;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void PostInitialize()
        {
            try
            {
                EnterState(_statesFactory.CreateState<LoadGameState>(), _cancellationTokenSource.Token).Forget();
            }
            catch (OperationCanceledException ex)
            {
                Debug.Log($"[match.states] Enter state canceled");
            }
        }

        private async UniTask EnterState(IState state, CancellationToken cancellationToken)
        {
            _activeState = state;
            Debug.Log($"<color=yellow>[states] enter {state.GetType().Name}.</color>");

            await _activeState.Enter(cancellationToken);
        }

        private async UniTask ExitState(IState state, CancellationToken cancellationToken)
        {
            Debug.Log($"<color=orange>[states] exit {state.GetType().Name}</color>");

            await _activeState.Exit(cancellationToken);
            _activeState.PostExit();
        }

        public void Tick()
        {
            if (_activeState == null)
            {
                return;
            }

            if (_activeState.IsCompleted)
            {
                if (!_activeState.IsExited)
                {
                    try
                    {
                        ExitState(_activeState, _cancellationTokenSource.Token).Forget();
                    }
                    catch (OperationCanceledException ex)
                    {
                        Debug.Log($"[match.states] Exit state canceled");
                    }

                    return;
                }

                try
                {
                    EnterState(_statesFactory.CreateState(_activeState.NextState), _cancellationTokenSource.Token)
                        .Forget();
                }
                catch (OperationCanceledException ex)
                {
                    Debug.Log($"[match.states] Enter state canceled");
                }
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}