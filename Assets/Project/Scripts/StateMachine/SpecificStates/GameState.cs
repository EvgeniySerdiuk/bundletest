using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.LoaderScreen.UIControllers;

namespace Project.Scripts.StateMachine.SpecificStates
{
    public class GameState : State
    {
        public override Type NextState => null;
        
        private readonly LoaderScreenUIController _loaderScreenUIController;

        public GameState(LoaderScreenUIController loaderScreenUIController)
        {
            _loaderScreenUIController = loaderScreenUIController;
        }

        public override UniTask Enter(CancellationToken cancellationToken)
        {
            _loaderScreenUIController.CloseScreen();
            return UniTask.CompletedTask;
        }

        public override UniTask Exit(CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }
    }
}