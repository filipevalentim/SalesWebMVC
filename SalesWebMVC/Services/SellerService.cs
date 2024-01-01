﻿using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using SalesWebMVC.Services.Exceptions;

namespace SalesWebMVC.Services
{
  public class SellerService
  {
    private readonly SalesWebMVCContext _context;
    public SellerService() { }
    public SellerService(SalesWebMVCContext context)
    {
      _context = context;
    }

    public async Task<List<Seller>> FindAllAsync()
    {
      return await _context.Seller.AsNoTracking().ToListAsync();
    }
    public async Task InsertAsync(Seller seller)
    {
      _context.Add(seller);
      await _context.SaveChangesAsync();
    }
    public async Task<Seller> FindByIdAsync(int id)
    {
      return await _context.Seller
          .Include(obj => obj.Department)
          .AsNoTracking()
          .FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task RemoveAsync(int id)
    {
      try
      {
        var obj = await _context.Seller.FindAsync(id);
        _context.Seller.Remove(obj);
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException e)
      {
        throw new IntegrityException(e.Message);
      }
    }

    public async Task UpdateAsync(Seller obj)
    {
      bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
      if (!hasAny)
      {
        throw new NotFoundException("Id not found!");
      }
      try
      {
        _context.Update(obj);
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException e)
      {
        throw new DbConcurrencyException(e.Message);
      }
    }
  }
}
