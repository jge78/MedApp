using PatientManagementConsumer.Data.Models;

namespace PatientManagementConsumer.Data
{
    public interface IStudyRepository
    {
        Study Get(int id);
        Study Add(Study study);
        Study Update(Study study);
        void Delete(int id);
    }
}
