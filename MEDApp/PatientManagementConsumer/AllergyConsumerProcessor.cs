using Newtonsoft.Json;
using PatientManagementConsumer.Data;
using PatientManagementConsumer.Data.Models;
using PatientManagementConsumer.Messaging;

namespace PatientManagementConsumer
{
    internal class AllergyConsumerProcessor : ConsumerProcessor
    {
        private IAllergyRepository allergyRepository;
        public AllergyConsumerProcessor(string dataConectionString) : base(dataConectionString)
        {
            allergyRepository = new AllergyRepository();
        }
        public override string ProcessMessage(Message messageToprocess)
        {
            string responseMessage;
            var idAllergy = messageToprocess.id;


            switch (messageToprocess.messageOperation)
            {
                case MessageEnums.MessageOperations.Add:
                    var tempAllergy = JsonConvert.SerializeObject(messageToprocess.payload);
                    Allergy allergy = JsonConvert.DeserializeObject<Allergy>(tempAllergy);

                    Allergy newAllergy = allergyRepository.Add(allergy);
                    responseMessage = JsonConvert.SerializeObject(newAllergy);
                    break;

                case MessageEnums.MessageOperations.Delete:
                    allergyRepository.Delete(Int32.Parse(idAllergy));
                    Allergy deletedAllergy = new() { Id = Int32.Parse(idAllergy) };
                    responseMessage = JsonConvert.SerializeObject(deletedAllergy);
                    break;

                case MessageEnums.MessageOperations.Get:
                    Allergy getAllergy = allergyRepository.Get(Int32.Parse(idAllergy));
                    responseMessage = JsonConvert.SerializeObject(getAllergy);
                    break;

                case MessageEnums.MessageOperations.Update:
                    var tempUpdateAllergy = JsonConvert.SerializeObject(messageToprocess.payload);
                    Allergy updateAllergy = JsonConvert.DeserializeObject<Allergy>(tempUpdateAllergy);

                    Allergy updated = allergyRepository.Update(updateAllergy);
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
