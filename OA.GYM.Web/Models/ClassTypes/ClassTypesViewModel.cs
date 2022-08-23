using System.ComponentModel.DataAnnotations;

namespace OA.GYM.Web.Models.ClassTypes
{
    public class ClassTypesViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Team")]
        public string Name { get; set; }

        public int Duration { get; set; }
    }
}
