using Wpm.Management.Domain.ValueObjects;
using Wpm.SharedKernel;

namespace Wpm.Management.Domain.Entities
{
    public class Breed : Entity
    {
        public string Name { get; init; } = string.Empty;
        public WeightRange MaleIdealWeight { get; init; }
        public WeightRange FamaleIdealWeight { get; init; }

        public Breed(Guid id, string name, WeightRange maleIdealWeight, WeightRange famaleIdealWeight)
        {
            Id = id;
            Name = name;
            MaleIdealWeight = maleIdealWeight;
            FamaleIdealWeight = famaleIdealWeight;
        }
    }
}
