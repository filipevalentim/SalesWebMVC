﻿namespace SalesWebMVC.Services;

using Data;
using Microsoft.EntityFrameworkCore;
using Models;

public class SalesRecordService
  {
    private readonly SalesWebMVCContext _context;
    public SalesRecordService(SalesWebMVCContext context)
    {
      _context = context;
    }

    public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
    {
      // Criando o objeto IQueryable:
      var result = from obj in _context.SalesRecords select obj;

      if (minDate.HasValue)
      {
        result = result.Where(x => x.Date >= minDate.Value);
      }
      if (maxDate.HasValue)
      {
        result = result.Where(x => x.Date <= maxDate.Value);
      }
      return await result
          .Include(x => x.Seller)
          .Include(x => x.Seller.Department)
          .OrderByDescending(x => x.Date)
          .ToListAsync();
    }
  }