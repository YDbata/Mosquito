using Mosquito.Stat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mosquito.UI
{
    public class HealthBar : MonoBehaviour
    {
        public Slider healthSlider;
        public TMP_Text healthBarText;


        MosquitoController playerDamageAble;

        private void Awake()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player)
            {
                playerDamageAble = player.GetComponent<MosquitoController>();
            }
        }

        private void OnEnable()
        {
            if (playerDamageAble)
            {
                playerDamageAble.Hp.onValueChanged.AddListener(OnPlayerHealthChanged);
            }
        }
        private void OnDisable()
        {
            if (playerDamageAble)
            {
                playerDamageAble.Hp.onValueChanged.RemoveListener(OnPlayerHealthChanged);
            }
        }


        private void Start()
        {
            healthSlider.value = CalculateSliderPercentage(playerDamageAble.Hp.value, playerDamageAble.Hp.MaxHp);
            //healthBarText.text = $"HP {playerDamageAble.Hp.value}/{playerDamageAble.Hp.MaxHp}";
        }

        private float CalculateSliderPercentage(float currentHealth, float maxHealth)
        {
            return currentHealth / maxHealth;
        }

        private void OnPlayerHealthChanged(float newHealth)
        {
            Debug.Log(healthSlider.value);
            float maxHealth = playerDamageAble.Hp.MaxHp;
            healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
            //healthBarText.text = $"HP {newHealth}/{maxHealth}";
        }
    }
}