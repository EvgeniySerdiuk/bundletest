using TMPro;
using UnityEngine;

namespace Project.Scripts.MainScreen.UI
{
    public class MainScreenUI : MonoBehaviour
    {
        [field: SerializeField] public MainScreenButtonUi UpCounterButton { get; private set; }
        [field: SerializeField] public MainScreenButtonUi RefreshContentButton { get; private set; }

        [SerializeField] private TMP_Text titleText;

        public void SetData(string title, int counterValue, Sprite buttonBg)
        {
            titleText.text = title;
            UpCounterButton.SetText(counterValue.ToString());
            UpCounterButton.image.sprite = buttonBg;
        }
    }
}