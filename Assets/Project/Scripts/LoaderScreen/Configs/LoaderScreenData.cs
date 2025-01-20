using Project.Scripts.LoaderScreen.UI;
using UnityEngine;

namespace Project.Scripts.LoaderScreen.Configs
{
    [CreateAssetMenu(menuName = "Game/" + nameof(LoaderScreenData))]
    public class LoaderScreenData : ScriptableObject
    {
        [field: SerializeField] public LoaderScreenUI LoaderScreenUI { get; private set; }
        [field: SerializeField] public float LoadingTime { get; private set; }
    }
}