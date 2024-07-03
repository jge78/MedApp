using AppointmentsConsumer.Data.Models;

namespace AppointmentsConsumer.Data
{
    internal interface IShiftRepository
    {
        Shift Get(int id);
        List<Shift> GetAll();
        Shift Add(Shift Shift);
        Shift Update(Shift Shift);
        void Delete(int id);
    }
}
