using Newtonsoft.Json;
using PatientManagementConsumer.Data;
using PatientManagementConsumer.Data.Models;
using PatientManagementConsumer.Messaging;

namespace PatientManagementConsumer
{
    internal class StudyConsumerProcessor : ConsumerProcessor
    {
        private IStudyRepository studyRepository;
        public StudyConsumerProcessor(string dataConectionString) : base(dataConectionString)
        {
            studyRepository = new StudyRepository();
        }
        public override string ProcessMessage(Message messageToprocess)
        {
            string responseMessage;
            var idStudy = messageToprocess.id;


            switch (messageToprocess.messageOperation)
            {
                case MessageEnums.MessageOperations.Add:
                    var tempStudy = JsonConvert.SerializeObject(messageToprocess.payload);
                    Study study = JsonConvert.DeserializeObject<Study>(tempStudy);

                    Study newStudy = studyRepository.Add(study);
                    responseMessage = JsonConvert.SerializeObject(newStudy);
                    break;

                case MessageEnums.MessageOperations.Delete:
                    studyRepository.Delete(Int32.Parse(idStudy));
                    Study deletedStudy = new() { Id = Int32.Parse(idStudy) };
                    responseMessage = JsonConvert.SerializeObject(deletedStudy);
                    break;

                case MessageEnums.MessageOperations.Get:
                    Study getStudy = studyRepository.Get(Int32.Parse(idStudy));
                    responseMessage = JsonConvert.SerializeObject(getStudy);
                    break;

                case MessageEnums.MessageOperations.Update:
                    var tempUpdateStudy = JsonConvert.SerializeObject(messageToprocess.payload);
                    Study updateStudy = JsonConvert.DeserializeObject<Study>(tempUpdateStudy);

                    Study updated = studyRepository.Update(updateStudy);
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
