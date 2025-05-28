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

        // crear un objeto breedId saltandose la validacion. esto es necesario para el dbcontext cuando llena un objeto breedId
        private BreedId(Guid value)
        {
            this.Value = value;
        }

        public static BreedId FromCreate(Guid value)
        {
            return new BreedId(value);
        }

        private void ValidateBreed(Guid value)
        {
            var result = _breedService.GetBreed(value);

            if (result == null)
                throw new ArgumentException("la raza no es valida");
        }
    }
}
