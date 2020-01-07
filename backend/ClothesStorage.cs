using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSkiColumbia
{
    public class ClothesStorage
    {
        StoragePerson men = new StoragePerson();
        StoragePerson women = new StoragePerson();
        StoragePerson youngGirl = new StoragePerson();
        StoragePerson youngBoy = new StoragePerson();

        public StoragePerson Men { get => men; set => men = value; }
        public StoragePerson Women { get => women; set => women = value; }
        public StoragePerson YoungGirl { get => youngGirl; set => youngGirl = value; }
        public StoragePerson YoungBoy { get => youngBoy; set => youngBoy = value; }

        public void Make_total_count_Item(Order toCount)
        {
            foreach (Suit temp in toCount.SuitsList)
            {
                Help_counter(temp);
            }
        }



        private void Help_counter(Suit temp)
        {
            StoragePerson toCount;
            if (temp.Jaket != null)
            {
                toCount = Get_the_storage_by_gender(temp.Jaket);
                toCount.Jaket[(int)temp.Jaket.Size]++;
            }
            
           
            if (temp.Pants != null)
            {
                toCount = Get_the_storage_by_gender(temp.Pants);
                toCount.Pants[(int)temp.Pants.Size]++;
            }
        }

        private StoragePerson Get_the_storage_by_gender(ClothingItem howIs) // need to convert to template
        {
            StoragePerson res;
            switch (howIs.Gender)
            {
                case GENDER.Men:
                    res =  men;
                    break;
                case GENDER.Women:
                    res = women;
                    break;
                case GENDER.Boy:
                    res = youngBoy;
                    break;
                case GENDER.Girl:
                    res = youngGirl;
                    break;
                default: // never will happened
                    res = men;
                    break;
            }
            return res;
        }

        private StoragePerson Get_the_storage_by_gender_for_current_clothing(ClothingItem howIs, ref StoragePerson menCheck, ref StoragePerson womenCheck, ref StoragePerson youngGirlCheck, ref StoragePerson youngBoyCheck)
        {
            StoragePerson res;
            switch (howIs.Gender)
            {
                case GENDER.Men:
                    res = menCheck;
                    break;
                case GENDER.Women:
                    res = womenCheck;
                    break;
                case GENDER.Boy:
                    res = youngBoyCheck;
                    break;
                case GENDER.Girl:
                    res = youngGirlCheck;
                    break;
                default: // never will happened
                    res = menCheck;
                    break;
            }
            return res;
        }

        private void count_for_current_suit(Suit suit, ref StoragePerson menCheck, ref StoragePerson womenCheck, ref StoragePerson youngGirlCheck, ref StoragePerson youngBoyCheck)
        {
            StoragePerson toCount;
            if (suit.Jaket != null)
            {
                toCount = Get_the_storage_by_gender_for_current_clothing(suit.Jaket, ref menCheck, ref womenCheck, ref youngGirlCheck, ref youngBoyCheck);
                toCount.Jaket[(int)suit.Jaket.Size]++;
            }
            if (suit.Pants != null)
            {
                toCount = Get_the_storage_by_gender_for_current_clothing(suit.Pants, ref menCheck, ref womenCheck, ref youngGirlCheck, ref youngBoyCheck);
                toCount.Pants[(int)suit.Pants.Size]++;
            }
        }

    
        public String Make_balance_Item(ClothesStorage storeStorage, Order toCheck)
        {
            string totalRes = string.Empty;
            StoragePerson menCheck = new StoragePerson();
            StoragePerson womenCheck = new StoragePerson();
            StoragePerson youngGirlCheck = new StoragePerson();
            StoragePerson youngBoyCheck = new StoragePerson();

            foreach (Suit suit in toCheck.SuitsList)
            {
                count_for_current_suit(suit, ref menCheck, ref womenCheck, ref youngGirlCheck, ref youngBoyCheck);
            }


            string isNull = string.Empty;

            isNull = Men.Make_balance_for_men_or_women(storeStorage.Men, menCheck);
            Update_string_balance_res(isNull, ref totalRes, "MEN: ");

            isNull = Women.Make_balance_for_men_or_women(storeStorage.Women, womenCheck);
            Update_string_balance_res(isNull, ref totalRes, "WOMEN: ");

            isNull = YoungBoy.Make_balance_girl_or_Boy(storeStorage.YoungBoy, storeStorage.YoungGirl, youngBoyCheck, YoungGirl);
            Update_string_balance_res(isNull, ref totalRes, "BOY: ");

            isNull = YoungGirl.Make_balance_girl_or_Boy(storeStorage.YoungGirl, storeStorage.YoungBoy, youngGirlCheck, YoungBoy);
            Update_string_balance_res(isNull, ref totalRes, "GIRL: ");

            return totalRes;
        }

        private void Update_string_balance_res(string res, ref string totalRes, string title)
        {

            if (res != string.Empty)
            {
                totalRes += title + "\n";
                totalRes += res;
            }
        }
    }
}
