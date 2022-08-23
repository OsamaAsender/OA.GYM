using OA.GYM.Entities;
using System.ComponentModel.DataAnnotations;

namespace OA.GYM.Web.Models.TrainingClasses
{
    public class TrainingClassesViewModel
    {
        public TrainingClassesViewModel()
        {
            Trainees = new List<Trainee>();
        }

        public int Id { get; set; }

        public double Price { get; set; }

        public DateTime StartTime { get; set; }

        public int ClassTypeId { get; set; }
        public ClassType? ClassType { get; set; }

        public int CoachId { get; set; }
        public Coach? Coach { get; set; }

        public List<Trainee> Trainees { get; set; }
    }
}
