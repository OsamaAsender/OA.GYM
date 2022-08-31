using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using OA.GYM.Entities;
using System.ComponentModel.DataAnnotations;

namespace OA.GYM.Web.Models.TrainingClasses
{
    public class TrainingClassesViewModel
    {
        public TrainingClassesViewModel()
        {
            TraineeIds = new List<int>();
        }

        public int Id { get; set; }

        public double Price { get; set; }

        [Display(Name ="Start Time")]
        public DateTime StartTime { get; set; }




        public int ClassTypeId { get; set; }
        [ValidateNever]
        public SelectList ClassTypeList { get; set; }


        public int CoachId { get; set; }
        [ValidateNever]
        public SelectList CoachesList { get; set; }


        public List<int> TraineeIds { get; set; }
        [ValidateNever]
        public MultiSelectList TraineesList { get; set; }
    }
}
