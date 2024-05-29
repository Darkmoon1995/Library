namespace LibraryTRY3.Models
{
    public class Borrow
    {
        public int ID { get; set; }
        public string UserThatGotTheBook { get; set; }
        public string BookName { get; set; }
        public DateTime DateBookTaken { get; set; }
        public DateTime DateBookGivenBack { get; set; }
    }
}
