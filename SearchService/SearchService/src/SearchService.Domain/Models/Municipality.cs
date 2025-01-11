using SearchService.Domain.Enums;
using SearchService.Domain.Mappers;

namespace SearchService.Domain.Models;

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