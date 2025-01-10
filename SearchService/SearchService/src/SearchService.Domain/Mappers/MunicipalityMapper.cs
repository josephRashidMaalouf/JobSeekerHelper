using SearchService.Domain.Enums;

namespace SearchService.Domain.Mappers;

public class MunicipalityMapper
{
    private static readonly Dictionary<SupportedMunicipalities, string> MunicipalityCodes = new()
    {
        { SupportedMunicipalities.Göteborg, "1480" },
        { SupportedMunicipalities.Gävle, "2180" },
        { SupportedMunicipalities.Luleå, "2580" },
        { SupportedMunicipalities.Lund, "1281" },
        { SupportedMunicipalities.Stenungsund, "1415" },
        { SupportedMunicipalities.Stockholm, "0180" },
        { SupportedMunicipalities.Borlänge, "2081" },
        { SupportedMunicipalities.Linköping, "0580" },
        { SupportedMunicipalities.Trollhättan, "1488" },
        { SupportedMunicipalities.Jönköping, "0680" },
        { SupportedMunicipalities.Malmö, "1280" },
        { SupportedMunicipalities.Uppsala, "0380" }
    };

    public static string GetCode(SupportedMunicipalities municipality)
    {
        return MunicipalityCodes[municipality];
    }
}