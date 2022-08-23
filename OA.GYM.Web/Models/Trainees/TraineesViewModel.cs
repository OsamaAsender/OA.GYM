using OA.GYM.Entities;
using OA.GYM.Utils.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.GYM.Web.Models.Trainees
{
    public class TraineesViewModel
    {
        public TraineesViewModel()
        {
           var TrainingClasses = new List<TrainingClass>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public List<TrainingClass> TrainingClasses { get; set; }
        
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
