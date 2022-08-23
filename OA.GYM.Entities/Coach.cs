using OA.GYM.Utils.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.GYM.Entities
{
    public class Coach
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DOB { get; set; }
        public string Profession { get; set; }
        public int CoachingTitles { get; set; }
        public Gender Gender { get; set; }
        public string Nationality { get; set; }
        public int Salary { get; set; }


        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [NotMapped]
        public int Age
        {
            get
            {
                if (DOB.HasValue)
                {
                    return DateTime.Now.Year - DOB.Value.Year;
                }
                else
                {
                    return -1;
                }
            }
        }
    }
    }
