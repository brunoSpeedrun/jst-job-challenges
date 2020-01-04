namespace Justa.Job.Backend.Api.Application.Services.DataValidation.Interfaces
{
    public interface ICpfValidator
    {
        bool Validate(string cpf);
    }
}