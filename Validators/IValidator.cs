using System.Threading.Tasks;

namespace WebApp.Validators
{
    public interface IValidator<in T>
    {
        Task<bool> Validate(T entity);
    }
}