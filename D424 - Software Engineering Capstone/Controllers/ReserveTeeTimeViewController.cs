using D424___Software_Engineering_Capstone.Models;
using System.Collections.ObjectModel;

namespace D424___Software_Engineering_Capstone.Controllers
{
    public class ReserveTeeTimeViewController
    {
        public ReserveTeeTimeViewController()
        {

        }

        public event Func<string, string, string, Task>? DisplayAlert;

        public ObservableCollection<int> PopulatePickerList(TeeTimeModel teeTime)
        {
            var list = new ObservableCollection<int>();

            for (var i = 1; i <= teeTime.AvailablePlayerSlots; i++)
            {
                list.Add(i);
            }

            return list;
        }

        public async Task<bool> ValidateReservationFirstName(string name)
        {
            if (!await Validation.ValidateUserFirstName(DisplayAlert, name))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ValidateReservationLastName(string name)
        {
            if (!await Validation.ValidateUserLastName(DisplayAlert, name))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ValidateReservationPhoneNumber(string number)
        {
            if (!await Validation.ValidateUserPhoneNumber(DisplayAlert, number))
            {
                return false;
            }

            return true;
        }

    }
}
