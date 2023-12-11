using System;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.GameCoupons.Samples.LocationSelection.Canvas
{
    internal class CityButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text _text;

        private Action<string> _onSelected;

        private void OnEnable() => _button.onClick.AddListener(OnButtonClicked);

        private void OnDisable() => _button.onClick.RemoveListener(OnButtonClicked);

        internal void Render(string name, Action<string> onSelected)
        {
            _text.text = name;
            _onSelected = onSelected;
        }

        private void OnButtonClicked()
        {
            _onSelected?.Invoke(_text.text);
        }
    }
}