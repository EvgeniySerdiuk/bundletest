using Project.Scripts.LoaderScreen.UI;
using Project.Scripts.MainScreen.UI;
using UnityEngine;

namespace Project.Scripts.LoaderScreen.Configs
{
    [CreateAssetMenu(menuName = "Game/" + nameof(ScreenContainer))]
    public class ScreenContainer : ScriptableObject
    {
        [field: SerializeField] public LoaderScreenUI LoaderScreenUI { get; private set; }
        [field: SerializeField] public MainScreenUI MainScreenUI { get; private set; }
        
        [field: SerializeField] public float LoadingTime { get; private set; }
    }
}