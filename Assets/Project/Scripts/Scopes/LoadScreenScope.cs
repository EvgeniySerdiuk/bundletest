using Project.Scripts.LoaderScreen.Configs;
using Project.Scripts.LoaderScreen.UIControllers;
using Project.Scripts.StateMachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.Scripts.Scopes
{
    public class LoadScreenScope : LifetimeScope
    {
        [SerializeField] private LoaderScreenData loaderScreenData;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.UseEntryPoints(epBuilder =>
            {
                epBuilder.Add<LoaderScreenUIController>().AsSelf();
                epBuilder.Add<ProjectStateMachine>().AsSelf();
            });
            
            builder.UseComponents(cBuilder =>
            {
                cBuilder.AddInstance(loaderScreenData);
            });
            
            StatesFactory.RegisterStates(builder);
            builder.Register<StatesFactory>(Lifetime.Scoped);
        }
    }
}