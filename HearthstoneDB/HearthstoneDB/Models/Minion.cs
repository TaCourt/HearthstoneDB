using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace HearthstoneDB.Models
{
    public class Minion : Card
    {
        private int m_strength;

        public int Strength
        {
            get { return m_strength; }
            set
            {
                m_strength = value;
                NotifyPropertyChanged("Strength");
            }
        }

        private int m_healthPoints;

        public int HealthPoints
        {
            get { return m_healthPoints; }
            set
            {
                m_healthPoints = value;
                NotifyPropertyChanged("HealthPoints");
            }
        }

        public Minion(String name, RarityType rarity, int cost, String effect, String story, String imagePath, int strength, int healthPoints)
            : base(name, rarity, cost, effect, story, imagePath)
        {
            Strength = strength;
            HealthPoints = healthPoints;
        }

        public Minion()
        {
            // TODO: Complete member initialization
        }
        
    }
}
