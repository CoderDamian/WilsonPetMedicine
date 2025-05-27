using Wpm.Management.Domain.Interfaces;
using Wpm.Management.Domain.ValueObjects;

namespace Wpm.Management.Domain.Entities
{
    public class Pet : Entity
    {
        public string Name { get; init; } = string.Empty;
        public int Age { get; init; }
        public string Color { get; init; } = string.Empty;
        public Weight Weight { get; private set; }
        public WeightState WeightState { get; private set; }
        public SexOfPet SexOfPet { get; init; }
        public BreedId BreedId { get; init; }

        public Pet(Guid guid, string name, int age, string color, Weight weight, SexOfPet sexOfPet, BreedId breedId)
        {
            Id = guid;
            Name = name;
            Age = age;
            Color = color;
            Weight = weight;
            SexOfPet = sexOfPet;
            BreedId = breedId;
        }

        public void SetWeight(Weight weight, IBreedService breedService)
        {
            Weight = weight;
            SetWeightState(breedService);
        }

        private void SetWeightState(IBreedService breedService)
        {
            var idealBreed = breedService.GetBreed(BreedId.Value);

            var (from, to) = SexOfPet switch
            {
                SexOfPet.Male => (idealBreed?.MaleIdealWeight.From, idealBreed?.MaleIdealWeight.To),
                SexOfPet.Female => (idealBreed?.FamaleIdealWeight.From, idealBreed?.FamaleIdealWeight.To),
                _ => throw new NotImplementedException()
            };

            WeightState = Weight.Value switch
            {
                _ when Weight.Value < from => WeightState.Underweight,
                _ when Weight.Value > to => WeightState.Overweight,
                _ => WeightState.Ideal
            };
        }
    }

    public enum SexOfPet
    {
        Male,
        Female
    }

    public enum WeightState
    {
        Unknow,
        Ideal,
        Underweight,
        Overweight
    }
}
