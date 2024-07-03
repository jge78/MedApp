using AppointmentsConsumer.Data;
using AppointmentsConsumer.Data.Models;
using AppointmentsConsumer.Messaging;
using Newtonsoft.Json;

namespace AppointmentsConsumer
{
    internal class ShiftConsumerProcessor : ConsumerProcessor
    {
        private ShiftRepository shiftRepository;
        public ShiftConsumerProcessor(string dataConectionString) : base(dataConectionString)
        {
            shiftRepository = new ShiftRepository(dataConectionString);
        }

        public override string ProcessMessage(Message messageToprocess)
        {
            string responseMessage = "";
            var idShift = messageToprocess.id;

            switch (messageToprocess.messageOperation)
            {
                case MessageEnums.MessageOperations.Add:
                    var tempAppointment = JsonConvert.SerializeObject(messageToprocess.payload);
                    Shift shift = JsonConvert.DeserializeObject<Shift>(tempAppointment);

                    Shift newShift = shiftRepository.Add(shift);
                    responseMessage = JsonConvert.SerializeObject(newShift);
                    break;

                case MessageEnums.MessageOperations.Delete:
                    shiftRepository.Delete(Int32.Parse(idShift));
                    Shift deletedShift = new Shift { Id = Int32.Parse(idShift) };
                    responseMessage = JsonConvert.SerializeObject(deletedShift);
                    break;

                case MessageEnums.MessageOperations.Get:
                    Shift getShift = shiftRepository.Get(Int32.Parse(idShift));
                    responseMessage = JsonConvert.SerializeObject(getShift);
                    break;

                case MessageEnums.MessageOperations.GetAll:
                    List<Shift> Shifts = shiftRepository.GetAll();
                    responseMessage = JsonConvert.SerializeObject(Shifts);
                    break;

                case MessageEnums.MessageOperations.Update:
                    var tempUpdateShift = JsonConvert.SerializeObject(messageToprocess.payload);
                    Shift updateShift = JsonConvert.DeserializeObject<Shift>(tempUpdateShift);

                    Shift updated = shiftRepository.Update(updateShift);
                    responseMessage = JsonConvert.SerializeObject(updated);
                    break;

                default:
                    responseMessage = "";
                    break;
            }

            return responseMessage;

        }


    }
}
