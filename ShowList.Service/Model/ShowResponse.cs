namespace ShowList.Service.Model
{
    public class ShowResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ShowResponse()
        {

        }
        public ShowResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}