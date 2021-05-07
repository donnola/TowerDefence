using System;
using RunTime;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGame
{
    public class GamePlayInfoUI : MonoBehaviour
    {
        [SerializeField]
        private Text m_HeathText;
        
        [SerializeField]
        private Text m_MoneyText;

        private void OnEnable()
        {
            SetHealth(Game.Player.Health);
            SetMoney(Game.Player.TurretMarket.Money);
            
            Game.Player.HealthChanged += SetHealth;
            Game.Player.TurretMarket.MoneyChanged += SetMoney;
        }

        private void OnDisable()
        {
            Game.Player.HealthChanged -= SetHealth;
            Game.Player.TurretMarket.MoneyChanged -= SetMoney;
        }

        private void SetHealth(int health)
        {
            m_HeathText.text = $"Health: {health}";
        }

        private void SetMoney(int money)
        {
            m_MoneyText.text = $"Money: {money}";
        }
    }
}