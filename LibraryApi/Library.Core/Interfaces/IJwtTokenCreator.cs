namespace Library.Core.Interfaces
{
    public interface IJwtTokenCreator
    {
        string Generate(string email, string userId);
    }
}