using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using System.IO;

namespace HearthstoneDB.Models
{
    [Serializable]
    public class Card : NotifyPropertyChangedBase
    {
        /// <summary>
        /// Attribut et propriété corresp
        /// </summary>

        public bool IsAMinion
        {
            get
            {
                if (this is Minion)
                    return true;
                else
                    return false;
            }
        }

        public enum RarityType { Common, Rare, Epic, Legendary }
        public RarityType CardRarity;
        public String Rarity
        {
            get
            {
                switch (CardRarity) 
                {
                    case RarityType.Common: return "Common";
                    case RarityType.Rare: return "Rare";
                    case RarityType.Epic: return "Epic";
                    case RarityType.Legendary: return "Legendary";
                    default: return "";
                }
            }
            set
            {
                switch (value)
                {
                    case "Common":
                        CardRarity = RarityType.Common;
                        break;
                    case "Rare":
                        CardRarity = RarityType.Rare;
                        break;
                    case "Epic":
                        CardRarity = RarityType.Epic;
                        break;
                    case "Legendary":
                        CardRarity = RarityType.Legendary;
                        break;
                    default: return;

                }
            }

        }





        private String m_name;

        public String Name
        {
            get { return m_name; }
            set
            {
                m_name = value;
                NotifyPropertyChanged("Name");

            }
        }


        private int m_cost;

        public int Cost
        {
            get { return m_cost; }
            set
            {
                m_cost = value;
                NotifyPropertyChanged("Cost");
            }
        }

        private String m_effect;

        public String Effect
        {
            get { return m_effect; }
            set
            {
                m_effect = value;
                NotifyPropertyChanged("Effect");
            }
        }

        private String m_story;

        public String Story
        {
            get { return m_story; }
            set
            {
                m_story = value;
                NotifyPropertyChanged("Story");
            }
        }

        private String m_imagePath;

        public String ImagePath 
        {
            get { return m_imagePath; }
            set
            {
                m_imagePath = value;
                NotifyPropertyChanged("ImagePath");
            }
        }

        public Card()
        {
        }

        public Card(String name, RarityType rarity, int cost, String effect, String story, String imagePath)
        {
            Name = name;
            CardRarity = rarity;
            Cost = cost;
            Effect = effect;
            Story = story;
            ImagePath = imagePath;
        }

        public override string ToString()
        {
            return string.Format("{0}", Name); 
        }

    }
}
