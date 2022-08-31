using OA.GYM.Web.Models.Trainees;

namespace OA.GYM.Web.Models.TrainingClasses
{
    public class TrainingClassListViewModel
    {
        public int Id { get; set; }

        public double Price { get; set; }

        public DateTime StartTime { get; set; }
        public string CoachFullName { get; set; }
        public string ClassTypeName { get; set; }
        public List<TraineesViewModel> Trainees { get; set; }
    }
}
