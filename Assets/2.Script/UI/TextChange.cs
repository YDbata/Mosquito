using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Mosquito.UI
{
    public class TextChange : MonoBehaviour
    {
        
        [Header("Stamina")]
        [SerializeField] private TextMeshProUGUI staminaAlert;


        public float waitTime = 0.7f;
        
        public static bool isStaminaAlert;
        

        private void Update()
        {
            if (isStaminaAlert)
            {
                AlertStamina();
            }
            
        }

        public void AlertStamina()
        {
            LocalizedString localizedString = new LocalizedString() { TableReference = "Stage", TableEntryReference = "alert_Stamina" };
            var stringOperation = localizedString.GetLocalizedStringAsync();
            if (stringOperation.IsDone && stringOperation.Status == AsyncOperationStatus.Succeeded) {
                staminaAlert.text = stringOperation.Result;
            }

            StartCoroutine(TextBlink(2, waitTime, staminaAlert));
            isStaminaAlert = false;
        }

        IEnumerator TextBlink(int count, float waitTime, TextMeshProUGUI text)
        {
            Color color = text.color;
            
            for (int i = 0; i < count; i++)
            {
                color.a = 1f;
                text.color = color;
                yield return new WaitForSeconds(waitTime);
                color.a = 0f;
                text.color = color;
                yield return new WaitForSeconds(waitTime);
            }
        }

    }
}