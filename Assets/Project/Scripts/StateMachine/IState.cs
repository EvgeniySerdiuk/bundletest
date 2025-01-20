using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Project.Scripts.StateMachine
{
    public interface IState
    {
        public Type NextState { get; }
        public bool IsCompleted { get; }
        public bool IsExited { get; }

        public UniTask Enter(CancellationToken cancellationToken);
        public UniTask Exit(CancellationToken cancellationToken);
        public void PostExit();
    }
}