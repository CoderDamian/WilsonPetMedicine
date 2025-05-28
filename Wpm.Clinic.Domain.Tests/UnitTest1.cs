namespace Wpm.Clinic.Domain.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Consulta_no_debe_finalizar_si_faltan_datos()
        {
            var consulta = new Consultation(Guid.NewGuid());
            Assert.Throws<InvalidOperationException>(consulta.End);
        }
    }
}