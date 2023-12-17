using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CardGame_Cons
{
    /// <summary> Заклинание атаки </summary>
    [DataContract]
    public class CardSpellAttack : Card
    {
        [DataMember]
        private int damage; // Урон, наносимый картой

        public CardSpellAttack(int price, string name, int damage_value)
        {
            Price = price; // Установка цены карты
            Name = name; // Установка названия карты
            damage = damage_value; // Установка значения урона

            DebugOff = false; // Изначально отладка включена
        }

        public void Damage(CardWarrior cardWarrior)
        {
            if (!DebugOff)
                Debug.Log($"Карта атаки '{Name}' нанесен урон {cardWarrior.CardStatus()} в размере {damage}"); // Вывод информации о нанесенном уроне
            cardWarrior.Damage(damage); // Нанесение урона карте-воину
        }

        public override CardType GetCardType
        {
            get
            {
                return CardType.Spell_Attack; // Получение типа карты (Заклинание атаки)
            }
        }

        public override string CardStatus()
        {
            return base.CardStatus() + $"Карта атаки '{Name}' Урон: {damage}"; // Получение статуса карты (с учетом названия и урона)
        }

    }
}
