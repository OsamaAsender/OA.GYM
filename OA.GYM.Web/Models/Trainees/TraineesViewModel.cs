using OA.GYM.Entities;
using OA.GYM.Utils.Enums;
using OA.GYM.Web.Models.TrainingClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.GYM.Web.Models.Trainees
{
    public class TraineesViewModel
    {
        public TraineesViewModel()
        {
            TrainingClasses = new List<TrainingClassesViewModel>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public List<TrainingClassesViewModel> TrainingClasses { get; set; }
        
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
