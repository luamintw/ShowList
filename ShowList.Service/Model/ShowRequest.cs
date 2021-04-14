namespace ShowList.Service.Model
{
    public class ShowRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ShowRequest() { }

        public ShowRequest(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}