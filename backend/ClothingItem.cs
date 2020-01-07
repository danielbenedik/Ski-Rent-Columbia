using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSkiColumbia
{

   public enum COMPENY { Columbia, IcePeak}
   public class ClothingItem
    {
        GENDER gender;
        SIZE size;
        COMPENY typeOfCompany;

        public ClothingItem(SIZE _size, COMPENY _typeOfCompany, GENDER _gender)
        {
            gender = _gender;
            size = _size;
            typeOfCompany = _typeOfCompany;
        }

        public COMPENY TypeOfCompany { get => typeOfCompany; set => typeOfCompany = value; }
        public SIZE Size { get => size; set => size = value; }
        public GENDER Gender { get => gender; set => gender = value; }
    }
}
