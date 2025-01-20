using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.LoaderScreen.UI
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private RectTransform progressBar;
        [SerializeField] private Image filledImage;

        private float _loadingTime;
        private Tween _tween;

        public void Construct(float loadingTime)
        {
            _loadingTime = loadingTime;
        }
        
        public void StartFillingBar(Action onComplete = null)
        {
            var progressBarMsxSizeX = progressBar.sizeDelta.x;
            _tween = filledImage.rectTransform.DOSizeDelta(new Vector2(progressBarMsxSizeX, filledImage.rectTransform.sizeDelta.y),
                _loadingTime).OnComplete(() => onComplete?.Invoke());
        }

        private void OnDisable()
        {
            _tween.Kill();
        }
    }
}
