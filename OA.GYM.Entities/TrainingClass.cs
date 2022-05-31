namespace OA.GYM.Entities
{
    public class TrainingClass
    {
        public TrainingClass()
        {
            Trainees = new List<Trainee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }

        public DateTime StartTime { get; set; }

        public int ClassTypeId { get; set; }
        public ClassType ClassType { get; set; }

        public int CoachId { get; set; }
        public Coach Coach { get; set; }

        public List<Trainee> Trainees { get; set; }
    }
}
