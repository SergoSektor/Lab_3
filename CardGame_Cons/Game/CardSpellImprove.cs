using System.Runtime.Serialization;

namespace CardGame_Cons
{
    [DataContract]
    /// <summary> Заклинание улучшения </summary>
    public class CardSpellImprove : Card
    {
        [DataMember]
        private int attack_mod; // Модификатор атаки
        [DataMember]
        private int health_mod; // Модификатор здоровья

        [DataMember]
        private Effects effect_mod; // Эффект модификации

        public CardSpellImprove()
        {
            Price = 0; // Установка цены карты
            Description = ""; // Установка описания карты
            DebugOff = false; // Изначально отладка включена
        }

        public CardSpellImprove(int price, string name, int attack_mod_value, int health_mod_value)
        {
            Price = price; // Установка цены карты
            Name = name; // Установка названия карты

            attack_mod = attack_mod_value; // Установка значения модификатора атаки
            health_mod = health_mod_value; // Установка значения модификатора здоровья

            effect_mod = Effects.Null; // Изначально эффект модификации отсутствует
            DebugOff = false; // Изначально отладка включена
        }

        public CardSpellImprove(int price, string name, int attack_mod_value, int health_mod_value, Effects effect)
        {
            Price = price; // Установка цены карты
            Name = name; // Установка названия карты

            attack_mod = attack_mod_value; // Установка значения модификатора атаки
            health_mod = health_mod_value; // Установка значения модификатора здоровья

            effect_mod = effect; // Установка эффекта модификации
            DebugOff = false; // Изначально отладка включена
        }

        public CardSpellImprove(int price, string name, Effects effect)
        {
            Price = price; // Установка цены карты
            Name = name; // Установка названия карты

            effect_mod = effect; // Установка эффекта модификации

            attack_mod = 0; // Изначально модификатор атаки равен 0
            health_mod = 0; // Изначально модификатор здоровья равен 0

            DebugOff = false; // Изначально отладка включена
        }

        public override CardType GetCardType
        {
            get
            {
                return CardType.Spell_Improve; // Получение типа карты (Заклинание улучшения)
            }
        }

        public void Modification(CardWarrior friendly_warrior)
        {
            if (!DebugOff)
                Debug.Log($"Карта '{Name}' модификация {friendly_warrior.CardStatus()} на величину {attack_mod}/{health_mod}"); // Вывод информации о модификации
            friendly_warrior.Modification(attack_mod, health_mod); // Применение модификации к союзному воину

            if (effect_mod != Effects.Null)
                friendly_warrior.Effect = effect_mod; // Установка эффекта модификации воину
        }

        public override string CardStatus()
        {
            string effect = string.Empty;
            switch (effect_mod)
            {
                case Effects.Hardness: effect = "и Стойкость"; break; // Если эффект модификации - "Стойкость"
                case Effects.Upgradable: effect = "и Грабитель"; break; // Если эффект модификации - "Грабитель"
                case Effects.Finite: effect = ", но Выгорание"; break; // Если эффект модификации - "Выгорание"
            }

            return base.CardStatus() + $"Карта усиления {Name} Усиление {attack_mod}/{health_mod} {effect}"; // Получение статуса карты (с учетом названия, модификаторов и эффекта модификации)
        }
    }
}
