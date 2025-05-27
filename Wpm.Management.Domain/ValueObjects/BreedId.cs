using Wpm.Management.Domain.Interfaces;

namespace Wpm.Management.Domain.ValueObjects
{
    public record BreedId
    {
        private readonly IBreedService _breedService;

        public Guid Value { get; init; }

        public BreedId(Guid value, IBreedService breedService)
        {
            this._breedService = breedService;
            ValidateBreed(value);
            Value = value;
        }

        private void ValidateBreed(Guid value)
        {
            var result = _breedService.GetBreed(value);

            if (result == null)
                throw new ArgumentException("la raza no es valida");
        }
    }
}
