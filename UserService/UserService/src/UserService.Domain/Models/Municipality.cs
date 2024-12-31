using UserService.Domain.Enums;
using UserService.Domain.Mappers;

namespace UserService.Domain.Models;

public class Municipality
{

    private SupportedMunicipalities _municipalityName;

    public required SupportedMunicipalities MunicipalityName
    {
        get => _municipalityName;
        set
        {
            _municipalityName = value;
            MunicipalityCode = MunicipalityMapper.GetCode(value);
        }
    }

    public string MunicipalityCode { get; set; } = string.Empty;
}