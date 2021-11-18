using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ABI.CCK.Scripts
{
    public class CCKLocalizationReplacer : MonoBehaviour
    {
        public string identifier;

        public void Start()
        {
            var tmpText = gameObject.GetComponent<TMP_Text>();
            if (tmpText != null)
                tmpText.text = CCKLocalizationProvider.GetLocalizedText(identifier);
            
            var text = gameObject.GetComponent<Text>();
            if (text != null)
                text.text = CCKLocalizationProvider.GetLocalizedText(identifier);
        }
    }
}