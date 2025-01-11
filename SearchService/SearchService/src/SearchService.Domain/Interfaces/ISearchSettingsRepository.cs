﻿using SearchService.Domain.Entities;
using SearchService.Domain.Models;

namespace SearchService.Domain.Interfaces;

public interface ISearchSettingsRepository : ICrud<SearchSettings>
{
    Task<Result<List<SearchSettings>>> GetAllByUserIdAsync(Guid userId);
}