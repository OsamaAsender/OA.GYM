using OA.GYM.Web.Models.Trainees;
using System.ComponentModel.DataAnnotations;

namespace OA.GYM.Web.Models.TrainingClasses
{
    public class TrainingClassListViewModel
    {
        public int Id { get; set; }

        public double Price { get; set; }

        public DateTime StartTime { get; set; }

        [Display(Name = "Coach")]
        public string CoachFullName { get; set; }

        [Display(Name ="Class")]
        public string ClassTypeName { get; set; }
        public List<TraineesViewModel> Trainees { get; set; }
    }
}
