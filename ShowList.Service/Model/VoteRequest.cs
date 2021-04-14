namespace ShowList.Service.Model
{
    public class VoteRequest
    {
        public string Phone { get; set; }

        public int[] ShowIds { get; set; }
    }
}