using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace CardGame_Cons
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Effects
    {
        Null, // Нет эффекта модификации
        Hardness, // Эффект "Стойкость"
        Upgradable, // Эффект "Грабитель"
        Finite // Эффект "Выгорание"
    }

    [DataContract]
    /// <summary> Карта существо </summary>
    public class CardWarrior : Card
    {
        [DataMember]
        private int attack; // Атака

        [DataMember]
        private int health; // Здоровье
        [DataMember]
        private int max_health; // Максимальное здоровье

        [DataMember]
        private bool alive; // Жив ли воин

        [DataMember]
        public Effects Effect { get; set; } // Эффект модификации воина

        public CardWarrior()
        {
            attack = 0; // Атака изначально равна 0
            max_health = 0; // Максимальное здоровье изначально равно 0
            health = 0; // Здоровье изначально равно 0

            Effect = Effects.Null; // Изначально нет эффекта модификации
            alive = false; // Изначально воин мертв
            DebugOff = false; // Изначально отладка включена
        }

        public CardWarrior(int price, string name, int attack_value, int max_healt_value, Effects warrior_effects)
        {
            Price = price; // Установка цены карты
            Name = name; // Установка названия карты
            attack = attack_value; // Установка значения атаки
            max_health = max_healt_value; // Установка значения максимального здоровья
            health = max_healt_value; // Установка значения текущего здоровья (равно максимальному)

            Effect = warrior_effects; // Установка эффекта модификации воина
            alive = true; // Воин жив
        }

        public override CardType GetCardType
        {
            get
            {
                return CardType.Warrior; // Получение типа карты (Существо)
            }
        }

        public bool GameStatus
        {
            get { return alive; } // Получение статуса воина (жив/мертв)
        }

        public void Death()
        {
            if (!DebugOff)
                Debug.Log($"{CardStatus()} смерть"); // Вывод информации о смерти воина

            alive = false; // Воин мертв
            attack = 0; // Обнуление атаки
            Effect = Effects.Null; // Обнуление эффекта модификации
            health = 0; // Обнуление здоровья
        }

        public int AttackPoints
        {
            get { return attack; }
            set { attack = (value <= 0) ? (0) : (value); } // Установка значения атаки (если значение меньше или равно 0, то устанавливается 0)
        }


        public void Modification(int attack_mod, int health_mod)
        {
            AttackPoints += attack_mod; // Модификация атаки

            max_health += health_mod; // Модификация максимального здоровья
            health += health_mod; // Модификация текущего здоровья
        }

        public void Damage(int damage_value)
        {
            if (!DebugOff)
                Debug.Log($"'{Name}'получает урон - {damage_value}"); // Вывод информации о получении урона воином

            health -= damage_value; // Уменьшение здоровья на значение урона

            if (Effect == Effects.Hardness) // Если эффект модификации воина - "Стойкость"
                health += 1; // Восстановление 1 единицы здоровья

            if (health <= 0) // Если здоровье меньше или равно 0
                Death(); // Воин умирает
        }

        public void Treatment(int treatment_value)
        {
            health += treatment_value; // Восстановление здоровья

            if (health > max_health) // Если текущее здоровье больше максимального
                health = max_health; // Установка текущего здоровья равным максимальному
        }

        public void AttackStatus()
        {
            switch (Effect)
            {
                case Effects.Upgradable: AttackPoints++; break; // Если эффект модификации - "Грабитель"
                case Effects.Finite: Damage(1); AttackPoints--; break; // Если эффект модификации - "Выгорание"
                default: break;
            }
        }

        public void DefendStatus()
        {
            switch (Effect)
            {
                case Effects.Finite: Damage(1); break; // Если эффект модификации - "Выгорание"
                default: break;
            }
        }

        public override string CardStatus()
        {
            string effect = string.Empty;
            switch (Effect)
            {
                case Effects.Hardness: effect = "Эффект: Стойкость"; break; // Если эффект модификации - "Стойкость"
                case Effects.Upgradable: effect = "Эффект: Грабитель"; break; // Если эффект модификации - "Грабитель"
                case Effects.Finite: effect = "Эффект: Выгорание"; break; // Если эффект модификации - "Выгорание"
            }
            return base.CardStatus() + $"'{Name}' {health}/{max_health} AP: {AttackPoints} {effect}"; // Получение статуса карты (с учетом названия, текущего здоровья, максимального здоровья, атаки и эффекта модификации)
        }

        public int Health
        {
            get { return health; } // Получение текущего здоровья
        }
    }
}
