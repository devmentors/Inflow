namespace Inflow.Shared.Infrastructure.Security.Encryption
{
    public interface IRng
    {
        string Generate(int length = 50, bool removeSpecialChars = true);
    }
}