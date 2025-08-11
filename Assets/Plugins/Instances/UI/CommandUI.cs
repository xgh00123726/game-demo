using GameBase.Tools;
using GameBase.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Instance.UI
{
    public class CommandUI : MonoBehaviour
    {
        public int maxTipNum = 10;

        private bool _lastInCmdMode = false;
        public bool _inCmdMode = false;
        private GameObject _tipText;
        private TextMeshProUGUI _tipTextTMP;
        private GameObject _inputText;
        public TMP_InputField _inputTextField;
        private string[] _tipTexts;

        private void SetElemActive(bool flag)
        {
            _tipText.SetActive(flag);
            _inputText.SetActive(flag);
        }

        private void UpdateTipText(string currInput)
        {
            var keys = Command.CommandKeys;

            _tipTextTMP.text = "";
            int currTipNum = 0;
            foreach (var key in keys)
            {
                if (currTipNum >= maxTipNum)
                {
                    break;
                }

                if (currInput == null || currInput.Length <= 1 || key.StartsWith(currInput))
                {
                    _tipTexts[currTipNum] = key;
                    currTipNum++;
                    _tipTextTMP.text += $"{key}\n";
                }
            }

            _inputTextField.ActivateInputField();
        }


        private void Awake()
        {
            transform.SetParent(RootCanvas.Instance.transform, false);
            _tipText = transform.Find("Text").gameObject;
            _tipTextTMP = _tipText.GetComponent<TextMeshProUGUI>();
            _inputText = transform.Find("Input").gameObject;
            _inputTextField = _inputText.GetComponent<TMP_InputField>();

            _inputTextField.onValueChanged.AddListener(UpdateTipText);

            _tipTexts = new string[maxTipNum];

            SetElemActive(false);
        }

        private void Update()
        {
            if (Inputs.GetKeyDown(KeyFunction.CmdConfirm))
            {
                _inCmdMode = !_inCmdMode;
                SetElemActive(_inCmdMode);
            }

            if (_inCmdMode && !_lastInCmdMode)
            {
                _inputTextField.ActivateInputField();
            }

            if (!_inCmdMode && _lastInCmdMode)
            {
                Command.Exec(_inputTextField.text);
                _inputTextField.text = "";
            }

            if (_inCmdMode && Inputs.GetKeyDown(KeyFunction.ChooseText))
            {
                _inputTextField.text = _tipTexts[0];
            }

            _lastInCmdMode = _inCmdMode;
        }
    }
}
