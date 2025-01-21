using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.MainScreen.UI
{
    public class MainScreenUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;
        
        [field: SerializeField] public MainScreenButtonUi UpCounterButton { get; private set; }
        [field: SerializeField] public MainScreenButtonUi RefreshContentButton { get; private set; }

        public void Construct(string title, int counterValue, Sprite buttonBg)
        {
            titleText.text = title;
            UpCounterButton.SetText(counterValue.ToString());
            UpCounterButton.image.sprite = buttonBg;
        }
    }
}