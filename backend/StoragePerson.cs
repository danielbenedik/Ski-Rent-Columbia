using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSkiColumbia
{
    public enum  SIZE {XXS, XS, S, M, L, XL, XXL};

    public class StoragePerson
    {
        int[] pants = new int[7];
        int[] jaket = new int[7];

        public int[] Pants { get => pants; set => pants = value; }
        public int[] Jaket { get => jaket; set => jaket = value; }

        public string Make_balance_for_men_or_women(StoragePerson storeGenderThatCheck, StoragePerson personToCheck)
        {
            string res = string.Empty;


            for (int i=0; i<7; i++)
            {
                res += Return_string_result("Jaket->", i, (storeGenderThatCheck.Jaket[i] - jaket[i]), personToCheck.Jaket[i]);

                res += Return_string_result("pants->", i, (storeGenderThatCheck.Pants[i] - pants[i]), personToCheck.Pants[i]);
            }

            return res;
        }

        public string Make_balance_girl_or_Boy(StoragePerson storeGenderThatCheck, StoragePerson storeOppositeGender, StoragePerson personToCheck, StoragePerson OppositeCountGender)
        {
            string res = string.Empty;

            for (int i = 0; i < 7; i++)
            {
                int a = storeGenderThatCheck.Jaket[i] - jaket[i];
                res += Return_string_result("Jaket->", i, (storeGenderThatCheck.Jaket[i] - jaket[i]), personToCheck.Jaket[i]);

                int excessClothing = storeOppositeGender.Pants[i] - OppositeCountGender.Pants[i];
                int balance = (storeGenderThatCheck.Pants[i] - pants[i]);

                if (excessClothing > 0) // if we have another clothing to give
                {
                    balance += excessClothing;
                }

                res += Return_string_result("pants->", i, balance, personToCheck.Pants[i]);
            }

            return res;
        }

        private string Return_string_result(string typeClothing, int size, int resultDifference, int ifNeed)
        {
            if ((resultDifference < 0) && (ifNeed > 0))
            {
                return (typeClothing + ((SIZE)size).ToString() + " the number of item in this: " + (resultDifference).ToString() + Environment.NewLine);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
