namespace Wpm.Clinic.Domain.ValueObjects
{
    public record PatientId
    {
        public Guid Value { get; init; }

        public PatientId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentNullException("value", "el identificador no puede ser vacio");

            Value = value;
        }

        public static implicit operator PatientId(Guid value)
            => new PatientId(value);
    }
}
