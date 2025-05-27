using Wpm.Management.Domain.Entities;

namespace Wpm.Management.Domain.Interfaces
{
    public interface IBreedService
    {
        Breed? GetBreed(Guid id);
    }
}
