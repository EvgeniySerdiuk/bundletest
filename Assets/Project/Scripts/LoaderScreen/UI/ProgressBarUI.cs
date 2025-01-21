using System;
using System.Threading;
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

        public void Construct(float loadingTime)
        {
            _loadingTime = loadingTime;
        }
        
        public UniTask StartFillingBar(CancellationToken token)
        {
            var progressBarMsxSizeX = progressBar.sizeDelta.x;
            return filledImage.rectTransform.DOSizeDelta(new Vector2(progressBarMsxSizeX, filledImage.rectTransform.sizeDelta.y),
                _loadingTime).ToUniTask(cancellationToken: token);
        }
    }
}
