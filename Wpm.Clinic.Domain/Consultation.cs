using Wpm.Clinic.Domain.ValueObjects;
using Wpm.SharedKernel;

namespace Wpm.Clinic.Domain;

public class Consultation : AggregateRoot
{
    private readonly List<DrugAdministration> _administeredDrugs = new();
    private readonly List<VitalSigns> _vitalSigns = new();

    public DateTime ConsultationStart { get; init; }
    public DateTime? ConsultationEnd { get; private set; }
    public PatientId PatientId { get; init; }
    public Text Diagnosis { get; private set; }
    public Text Treatment { get; private set; }
    public Weight CurrentWeight { get; private set; }
    public ConsultationStatus Status { get; private set; }
    public IReadOnlyCollection<DrugAdministration> AdministeredDrugs => _administeredDrugs;
    public IReadOnlyCollection<VitalSigns> VitalSignsReadings => _vitalSigns;

    public Consultation(PatientId patientId)
    {
        Id = Guid.NewGuid();
        PatientId = patientId;
        Status = ConsultationStatus.Started;
        ConsultationStart = DateTime.UtcNow;
    }

    public void SetWeight(Weight weight)
    {
        ValidateConsulationStatus();
        CurrentWeight = weight;
    }

    public void SetDiagnosis(Text diagnosis)
    {
        ValidateConsulationStatus();
        Diagnosis = diagnosis;
    }

    public void SetTreatment(Text treatment)
    {
        ValidateConsulationStatus();
        Treatment = treatment;
    }

    public void AdministerDrug (DrugId drugId, Dose dose)
    {
        ValidateConsulationStatus();
        var newDrugAdministration = new DrugAdministration(drugId, dose);
        _administeredDrugs.Add(newDrugAdministration);  
    }

    public void End()
    {
        ValidateConsulationStatus();

        if (Diagnosis == null || Treatment == null || CurrentWeight == null)
            throw new InvalidOperationException("la consulta no puede ser finalizada");
        
        Status = ConsultationStatus.Finalized;
        ConsultationEnd= DateTime.UtcNow;   
    }

    public void RegisterVitalSigns(IEnumerable<VitalSigns> vitalSigns)
    {
        ValidateConsulationStatus();
        _vitalSigns.AddRange(vitalSigns);
    }

    private void ValidateConsulationStatus()
    {
        if (Status == ConsultationStatus.Finalized)
            throw new InvalidOperationException("la consulta esta finalizada");
    }
}

public enum ConsultationStatus
{
    Started,
    Finalized
}
