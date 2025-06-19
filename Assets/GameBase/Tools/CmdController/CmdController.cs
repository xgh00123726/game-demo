using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

namespace GameBase.Tools
{
    public class CmdController : MonoBehaviour
    {
        public static bool _cmdActive;
        public static TMP_InputField _inputField;
        public static GameObject _UIComponents;
        public static Dictionary<string, Action> _cmdDict = new Dictionary<string, Action> { };
        private static void ToggleUI()
        {
            if (_UIComponents.activeSelf)
            {
                _UIComponents.SetActive(false);
                _cmdActive = false;
            }
            else
            {
                _UIComponents.SetActive(true);
                _cmdActive = true;
            }
        }

        private static void Confirm()
        {
            Debug.Log($"text is :{_inputField.text}");
            if (_cmdDict.ContainsKey(_inputField.text))
            {
                _cmdDict[_inputField.text]?.Invoke();
            }
        }

        public static void SetCmd(string cmdStr, Action cmdAction)
        {
            _cmdDict[cmdStr] = cmdAction;
        }

        // Start is called before the first frame update
        protected void Start()
        {
            _UIComponents = transform.Find("UIComponents").gameObject;
            if (_UIComponents == null)
            {
                Debug.LogWarning("your UI may lost some components");
            }

            _inputField = _UIComponents.transform.Find("InputField").GetComponent<TMP_InputField>();
            if (_inputField == null)
            {
                Debug.LogWarning("input field is null");
            }
        }

        // Update is called once per frame
        protected void Update()
        {
            if (Tools.Inputs.GetKeyDown(KeyFunction.ToggleCmd))
            {
                ToggleUI();
            }
            if (Tools.Inputs.GetKeyDown(KeyFunction.CmdConfirm))
            {
                Confirm();
            }
        }
    }
}
