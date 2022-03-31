namespace interview.Contracts.Requests
{
    public class CreateAuthorRequest
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }
    }
}