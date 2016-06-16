using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace HearthstoneDB.Models
{
    public class Spell : Card
    {
        public Spell(String name, RarityType rarity, int cost, String effect, String story, String imagePath)
            : base(name, rarity, cost, effect, story, imagePath)
        {

        }

        public Spell()
        {

        }

    }
}
