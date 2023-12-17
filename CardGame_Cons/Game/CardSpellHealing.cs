using System.Runtime.Serialization;

namespace CardGame_Cons
{
    [DataContract]
    /// <summary> Заклинание лечения </summary>
    public class CardSpellHealing : Card
    {
        [DataMember]
        private int treatment; // Величина лечения, наносимая картой

        public CardSpellHealing(int price, string name, int treatment_value)
        {
            Price = price; // Установка цены карты
            Name = name; // Установка названия карты
            treatment = treatment_value; // Установка значения лечения

            DebugOff = false; // Изначально отладка включена
        }

        public override CardType GetCardType
        {
            get
            {
                return CardType.Spell_Healing; // Получение типа карты (Заклинание лечения)
            }
        }

        public void Treatment(CardWarrior cardWarrior)
        {
            if (!DebugOff)
                Debug.Log($"Карта '{Name}' лечение {cardWarrior.CardStatus()} на величину {treatment}"); // Вывод информации о лечении
            cardWarrior.Treatment(treatment); // Применение лечения к карте-воину
        }

        public override string CardStatus()
        {
            return base.CardStatus() + $"Карта лечения '{Name}' Лечение: {treatment}"; // Получение статуса карты (с учетом названия и величины лечения)
        }
    }
}
