using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTAP.Item_Scripts.Traps
{
    static class StatusEffectTraps
    {
        public static void Gunked()
        {
            Player.singlePlayer.TakeAffliction(HitEvent.AFFLICTIONTYPE.GUNK, 100f);
        }
        public static void Scoured()
        {
            Player.singlePlayer.TakeAffliction(HitEvent.AFFLICTIONTYPE.SCOUR, 100f);
        }
        public static void Hypnotized()
        {
            Player.singlePlayer.TakeAffliction(HitEvent.AFFLICTIONTYPE.HYPNOSIS, 100f, Player.singlePlayer);
        }
        public static void Bleached()
        {
            Player.singlePlayer.TakeAffliction(HitEvent.AFFLICTIONTYPE.BLEACH, 100f);
            
        }
        public static void Afraid()
        {
            Player.singlePlayer.TakeAffliction(HitEvent.AFFLICTIONTYPE.FEAR, 100f);

        }
        public static void Electrocute()
        {
            Player.singlePlayer.Electrocute(5f);
        }

        public static void PoisonCocktail()
        {
            Random rand = new Random();

            if (rand.Next(0,2) != 0)
            {
                Gunked();
            }
            if (rand.Next(0, 2) != 0)
            {
                Bleached();
            }
            if (rand.Next(0, 2) != 0)
            {
                Scoured();
            }
            if (rand.Next(0, 2) != 0)
            {
                Electrocute();
            }
        }
    }
}
