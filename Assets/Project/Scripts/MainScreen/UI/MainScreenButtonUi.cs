using TMPro;
using UnityEngine.UI;

namespace Project.Scripts.MainScreen.UI
{
    public class MainScreenButtonUi : Button
    {
        private TMP_Text _buttonText;
        private string _text;

        public void SetText(string newText)
        {
            if (_buttonText == null)
            {
                _buttonText = GetComponentInChildren<TMP_Text>();
                _text = _buttonText.text;
            }

            _buttonText.text = $"{_text} : {newText}";
        }
    }
}