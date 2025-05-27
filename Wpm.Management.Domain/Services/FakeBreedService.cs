using Wpm.Management.Domain.Entities;
using Wpm.Management.Domain.Interfaces;
using Wpm.Management.Domain.ValueObjects;

namespace Wpm.Management.Domain.Services
{
    public class FakeBreedService : IBreedService
    {
        public readonly List<Breed> breeds =
            [
                new Breed(Guid.NewGuid(), "Beagle", new WeightRange(10m, 11m), new WeightRange(9m, 10m)),
                new Breed(Guid.NewGuid(), "Runo", new WeightRange(28m, 32m), new WeightRange(24m, 27m))
            ];

        public Breed? GetBreed(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("id no puede ser vacio o nulo");

            Breed? breed = breeds.Find(b => b.Id == id);

            return breed ?? throw new ArgumentException($"{id} no es valida");
        }
    }
}
