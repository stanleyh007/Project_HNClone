namespace Project_HNClone.Data
{
    public class Story
    {
        public int id { get; set; }
        public string name { get; set; }
        public string content { get; set; }
        public int creatorID { get; set; }
        public string creatorName { get; set; }
        public string postType { get; set; }
        public string postURL { get; set; }
        public int positiveRating { get; set; }
        public int negativeRating { get; set; }
        public string publishDate { get; set; }

    }
}
