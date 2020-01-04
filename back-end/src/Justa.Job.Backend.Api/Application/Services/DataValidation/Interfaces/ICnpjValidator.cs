namespace Justa.Job.Backend.Api.Application.Services.DataValidation.Interfaces
{
    public interface ICnpjValidator
    {
         bool Validate(string cnpj);
    }
}