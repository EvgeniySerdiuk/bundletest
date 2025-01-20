using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Project.Scripts.StateMachine
{
    public abstract class State : IState
    {
        public abstract Type NextState { get; }

        public bool IsCompleted { get; private set; }
        public bool IsExited { get; private set; }

        protected void Complete()
        {
            IsCompleted = true;
        }

        public void PostExit()
        {
            IsExited = true;
        }
        
        public abstract UniTask Enter(CancellationToken cancellationToken);
        public abstract UniTask Exit(CancellationToken cancellationToken);
    }
}