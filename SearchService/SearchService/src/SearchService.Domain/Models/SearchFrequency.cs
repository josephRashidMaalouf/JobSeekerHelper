using SearchService.Domain.Enums;
using SearchService.Domain.Exceptions;

namespace SearchService.Domain.Models;

public class SearchFrequency
{
    private int _frequency;

    public required int Frequency
    {
        get => _frequency;
        set
        {
            if (value < 1)
            {
                throw new InvalidFrequencyException("Frequency must be 1 or higher");
            }
            _frequency = value;
        }
    }

    public required IntervalOption Interval { get; set; }
}