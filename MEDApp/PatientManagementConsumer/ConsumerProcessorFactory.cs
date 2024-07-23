namespace PatientManagementConsumer
{
    public class ConsumerProcessorFactory
    {
        public static ConsumerProcessor Create(string messageEntity, string dataConectionString)
        {
            switch (messageEntity.Substring(messageEntity.LastIndexOf(".") + 1))
            {
                //TODO: Implement based on the requirements
                case "AppointmentHistory":
                    return new AppointmentHistoryConsumerProcessor(dataConectionString);
                case "Allergy":
                    return new AllergyConsumerProcessor(dataConectionString);
                default:
                    //TODO:Implement NULL
                    return new AppointmentHistoryConsumerProcessor(dataConectionString);
                    //break;
            }
        }

    }
}
