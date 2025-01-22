using BundleTest.Project.Scripts.JsonUtility;
using BundleTest.Project.Scripts.MainScreen;
using BundleTest.Project.Scripts.MainScreen.Counter;
using BundleTest.Project.Scripts.MainScreen.TittleText;
using Project.Scripts.AssetBundlesUtility;
using Project.Scripts.LoaderScreen.Configs;
using Project.Scripts.LoaderScreen.UIControllers;
using Project.Scripts.MainScreen.UIControllers;
using Project.Scripts.SaveLoad;
using Project.Scripts.StateMachine;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace Project.Scripts.Scopes
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private ScreenContainer screenContainer;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.UseEntryPoints(epBuilder =>
            {
                epBuilder.Add<ProjectStateMachine>().AsSelf();
                epBuilder.Add<LoaderScreenUIController>().AsSelf();
            });

            builder.UseComponents(cBuilder =>
            {
                cBuilder.AddInstance(screenContainer);
            });

            StatesFactory.RegisterStates(builder);

            builder.Register<SaveService>(Lifetime.Singleton);
            builder.Register<StatesFactory>(Lifetime.Scoped);
            builder.Register<RemoteAssetBundleLoader>(Lifetime.Scoped);
            builder.Register<MainScreenCounterService>(Lifetime.Scoped);
            builder.Register<MainScreenTitleTextService>(Lifetime.Scoped);
            builder.Register<MainScreenButtonBgService>(Lifetime.Scoped);
            builder.Register<RemoteJsonLoader>(Lifetime.Scoped);
            builder.Register<MainScreenUIController>(Lifetime.Scoped);
        }
    }
}