using Wpm.Management.Domain.Entities;
using Wpm.Management.Domain.Services;
using Wpm.Management.Domain.ValueObjects;
using Wpm.SharedKernel;

namespace Wpm.Management.Domain.Tests;

public class UnitTest1
{
    [Fact]
    public void Pet_debe_ser_igual()
    {
        var guid = Guid.NewGuid();

        var breedService = new FakeBreedService();
        var raceId = breedService.breeds[0].Id;
        var breedId = new BreedId(raceId, breedService);

        var pet1 = new Pet(guid, "aby", 3, "blue", new Weight(10.5m), SexOfPet.Male, breedId);
        var pet2 = new Pet(guid, "mark", 5, "yellow", new Weight(12.6m), SexOfPet.Male, breedId);   

        // sin utilizar la sobrecarga == definida en la clase
        Assert.True(pet1.Equals(pet2));

        // utilizando la sobrecarga == definida en la clase
        Assert.True(pet1 == pet2);
    }

    [Fact]
    public void BreedId_debe_ser_valido()
    {
        FakeBreedService fakeBreed = new FakeBreedService();
        Guid guid = fakeBreed.breeds[0].Id;

        BreedId breedId = new BreedId(guid, fakeBreed);
        Assert.NotNull(breedId);
    }

    [Fact]
    public void WeightState_debe_ser_ideal()
    {
        var guid = Guid.NewGuid();

        var breedService = new FakeBreedService();
        var raceId = breedService.breeds[0].Id;
        var breedId = new BreedId(raceId, breedService);

        var pet1 = new Pet(guid, "benji", 3, "blue", new Weight(10.5m), SexOfPet.Male, breedId);

        var newWeight = new Weight(10m);
        pet1.SetWeight(newWeight, breedService);

        Assert.True(pet1.WeightState == WeightState.Ideal);
    }
}