using PatientManagementConsumer.Data.Models;

namespace PatientManagementConsumer.Data
{
    internal class StudyRepository : IStudyRepository
    {
        public Study Add(Study study)
        {
            //TODO: Remove mock
            var newStudyRecord = new Study
            {
                Id = 99,
                Patient = study.Patient,
                Date = study.Date,
                StudyName = study.StudyName,
                FilePath = study.FilePath
            };

            return newStudyRecord;
        }

        public void Delete(int id)
        {
            //TODO: Implement
            //throw new NotImplementedException();
            return;
        }

        public Study Get(int id)
        {
            //TODO: Remove mock
            var newStudyRecord = new Study
            {
                Id = id,
                Patient = 3,
                Date = DateTime.Now,
                StudyName = "Initial blood test",
                FilePath = ""
            };

            return newStudyRecord;
        }

        public Study Update(Study study)
        {
            //TODO: Remove mock
            var newStudyRecord = new Study
            {
                Id = study.Id,
                Patient = study.Patient,
                Date = study.Date,
                StudyName = study.StudyName + " This is an update!",
                FilePath = study.FilePath
            };

            return newStudyRecord;
        }
    }
}
