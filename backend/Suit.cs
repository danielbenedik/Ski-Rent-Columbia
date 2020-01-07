using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSkiColumbia
{
   public enum GENDER { Men, Women, Boy, Girl};

    public class Suit
    {
        ClothingItem jaket; // null for empty
        ClothingItem pants; // null for empty
        

        public Suit(ClothingItem _pant, ClothingItem _jaket)
        {
            pants = _pant;
            jaket = _jaket;
        }

        public ClothingItem Pants { get => pants; set => pants = value; }
        public ClothingItem Jaket { get => jaket; set => jaket = value; }
    }
}
