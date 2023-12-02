using UnityEngine;
using UnityEngine.UI;

namespace Agava.GameCoupons.Samples.Playtesting
{
    public class LoginGroup : MonoBehaviour
    {
        [SerializeField] private InputField _idInput;
        [SerializeField] private InputField _passwordInput;
        [SerializeField] private Button _loginButton;
        [SerializeField] private Text _loginText;

        private void OnEnable()
        {
            _loginButton.onClick.AddListener(OnLoginButtonClicked);
        }

        private void OnDisable()
        {
            _loginButton.onClick.RemoveListener(OnLoginButtonClicked);
        }

        private async void OnLoginButtonClicked()
        {
            var success = await GameCoupons.Login(int.Parse(_idInput.text), _passwordInput.text, (error) => Debug.Log(error));

            if (success)
            {
                _loginText.text = "Authorized!";
                _loginText.color = Color.green;
            }
            else
            {
                _loginText.text = "Not authorized!";
                _loginText.color = Color.red;
            }
        }
    }
}
