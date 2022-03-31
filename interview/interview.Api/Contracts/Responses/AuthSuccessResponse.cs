namespace interview.Contracts.Responses
{
    public class AuthSuccessResponse
    {
        public AuthSuccessResponse(string token)
        {
            Token = token;
        }
        
        public string Token { get; set; }
    }
}