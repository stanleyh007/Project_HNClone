namespace Project_HNClone.Data
{
    public class Comment
    {
        public int id { get; set; }
        public string content { get; set; }
        public int ownerID { get; set; }
        public int storyID { get; set; }
        public string publishDate { get; set; }

    }
}
