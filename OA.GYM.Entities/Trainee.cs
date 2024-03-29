﻿using OA.GYM.Utils.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.GYM.Entities
{
    public class Trainee
    {
        public Trainee()
        {
            TrainingClasses = new List<TrainingClass>();
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
