using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CardGame_Cons
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CardType
    {
        CardNull, // Карта неопределенного типа
        Warrior, // Воин
        Spell_Improve, // Заклинание улучшения
        Spell_Attack, // Заклинание атаки
        Spell_Healing // Заклинание исцеления
    }

    [DataContract]
    public class Card
    {
        [DataMember]
        public string Name { get; set; } // Название карты

        [DataMember]
        public int Price { get; set; } // Цена карты

        [DataMember]
        /// <summary> Описание карты </summary>
        public string Description { get; set; } // Описание карты

        [DataMember]
        /// <summary> Отключение вывода статистики </summary>
        public bool DebugOff { get; set; } // Флаг для отключения вывода статистики

        public virtual CardType GetCardType
        {
            get { return CardType.CardNull; } // Получение типа карты
        }

        public virtual string CardStatus()
        {
            return $"({Price})"; // Возвращение статуса карты в формате: (Цена)
        }

        public Card()
        {
            Price = 0;
            Description = "";
        }
    }
}
