namespace SailMapper.Classes
{
    public class Rating
    {
        public Rating() { }
        public int Id { get; set; }
        public int BaseRating { get; set; }
        public int SpinnakerAdjustment { get; set; }
        public int Adjustment { get; set; }

        // Navigation property if you want to navigate from Rating to Boat
        public ICollection<Boat>? Boats { get; set; } // This allows multiple boats to have the same rating
    } 
}
