using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Project.Scripts.StateMachine.SpecificStates
{
    public class LoadGameState : State
    {
        public override Type NextState { get; }

        public LoadGameState()
        {
            
        }
        
        public override UniTask Enter(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public override UniTask Exit(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}