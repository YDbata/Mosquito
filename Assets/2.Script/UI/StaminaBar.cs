using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mosquito.UI
{
    public class StaminaBar : MonoBehaviour
    {
        public Slider Slider;


        MosquitoController player;

        private void Awake()
        {
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            if (Player)
            {
                player = Player.GetComponent<MosquitoController>();
            }
        }

        private void OnDisable()
        {
            if (player)
            {
                player.stamina.onValueChanged-=(OnPlayerStaminaChanged);
            }
        }


        private void Start()
        {
            Slider.value = CalculateSliderPercentage(player.stamina.value, player.stamina.MaxStamina);
            if (player)
            {
                player.stamina.onValueChanged+=(OnPlayerStaminaChanged);
            }
            //healthBarText.text = $"HP {playerDamageAble.Hp.value}/{playerDamageAble.Hp.MaxHp}";
        }

        private float CalculateSliderPercentage(float current, float max)
        {
            return current / max;
        }

        private void OnPlayerStaminaChanged(float newvalue)
        {
            //Debug.Log(Slider.value);
            float max = player.stamina.MaxStamina;
            Slider.value = CalculateSliderPercentage(newvalue, max);
            //healthBarText.text = $"HP {newHealth}/{maxHealth}";
        }
    }
    
}