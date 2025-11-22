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
        private string[] _tipTexts;
        private GameObject _tipText;
        private TextMeshProUGUI _tipTextTMP;
        private GameObject _inputText;
        private int _currTipNum = 0;
        private bool _lastInCmdMode = false;
        private string _lastCmd;
        private bool _inCmdMode = false;
        private TMP_InputField _inputTextField;

        public int MaxTipNum { get; set; } = 10;

        private void SetElemActive(bool flag)
        {
            _tipText.SetActive(flag);
            _inputText.SetActive(flag);
        }

        /// <summary>
        /// 更新cmd面板上方的提示文字
        /// </summary>
        /// <param Name="currInput"></param>
        private void UpdateTipText(string currInput)
        {
            var keys = Command.CommandKeys;

            _tipTextTMP.text = "";
            _currTipNum = 0;
            foreach (var key in keys)
            {
                if (_currTipNum >= MaxTipNum)
                {
                    break;
                }

                if (currInput == null || currInput.Length <= 1 || key.StartsWith(currInput))
                {
                    _tipTexts[_currTipNum] = key;
                    _currTipNum++;
                    _tipTextTMP.text += $"{key}\n";
                }
            }

            _inputTextField.ActivateInputField();
        }


        private void Awake()
        {
            transform.SetParent(RootCanvas.Instance.Layer(3), false);

            _tipText = transform.Find("Text").gameObject;
            _tipTextTMP = _tipText.GetComponent<TextMeshProUGUI>();
            _inputText = transform.Find("Input").gameObject;
            _inputTextField = _inputText.GetComponent<TMP_InputField>();

            _inputTextField.onValueChanged.AddListener(UpdateTipText);

            _tipTexts = new string[MaxTipNum];

            SetElemActive(false);
        }

        private void OnActiveCmd()
        {
            _inputTextField.ActivateInputField();
            Inputs.LockOthers("cmd");
        }

        private void OnDeactiveCmd()
        {
            Command.Exec(_inputTextField.text);
            _lastCmd = _inputTextField.text;
            _inputTextField.text = "";
            Inputs.UnlockAll();
        }

        private void Update()
        {
            if (Inputs.GetKeyDown(KeyFunction.CmdConfirm, "cmd"))
            {
                _inCmdMode = !_inCmdMode;
                SetElemActive(_inCmdMode);
            }

            // 如果进入命令模式，就激活控制台
            if (_inCmdMode && !_lastInCmdMode)
            {
                OnActiveCmd();
            }

            if (_inCmdMode && Inputs.GetKeyDown(KeyFunction.CmdUseLast, "cmd"))
            {
                _inputTextField.text = _lastCmd;
            }

            // 如果控制台退出，则执行指令
            if (!_inCmdMode && _lastInCmdMode)
            {
                OnDeactiveCmd();
            }

            if (_inCmdMode && Inputs.GetKeyDown(KeyFunction.ChooseText, "cmd"))
            {
                _inputTextField.text = _tipTexts[_currTipNum - 1];
                _inputTextField.caretPosition = _inputTextField.text.Length + 1;
            }

            _lastInCmdMode = _inCmdMode;
        }
    }
}
