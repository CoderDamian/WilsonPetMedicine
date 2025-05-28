namespace Wpm.Clinic.Domain.ValueObjects
{
    public record Dose
    {
        public decimal Quantity { get; set; }
        public UnitOfMeasure Unit { get; set; }

        public Dose(decimal quantity, UnitOfMeasure unit)
        {
            Quantity = quantity;
            Unit = unit;
        }
    }
}

public enum UnitOfMeasure
{
    mg,
    ml,
    tablet
}
