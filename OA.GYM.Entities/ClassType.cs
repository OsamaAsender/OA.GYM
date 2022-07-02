namespace OA.GYM.Entities
{
    public class ClassType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public int CoachId { get; set; }
        public Coach coach { get; set; }
    }
}
