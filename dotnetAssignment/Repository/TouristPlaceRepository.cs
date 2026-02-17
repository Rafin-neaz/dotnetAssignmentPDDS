using dotnetAssignment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetAssignment.Repository
{
    public class TouristPlaceRepository : ITouristPlaceRepository
    {
        private readonly AppDbContext _context;

        public TouristPlaceRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // 1. Asynchronous Create
        public async Task<TouristPlace> CreateNew(TouristPlace place)
        {
            if (place == null) throw new ArgumentNullException(nameof(place));

            await _context.Tourists.AddAsync(place);
            await _context.SaveChangesAsync();

            return place;
        }

        // 2. Asynchronous Delete with explicit null handling
        public async Task<TouristPlace> Delete(long id)
        {
            var place = await _context.Tourists.FindAsync(id);
            if (place == null) return null;

            _context.Tourists.Remove(place);
            await _context.SaveChangesAsync();

            return place;
        }

        // 3. Optimized Read-only Get
        public async Task<TouristPlace> Get(long id)
        {
            return await _context.Tourists
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        // 4. Fixed naming (GetAll) and Asynchronous List
        public async Task<List<TouristPlace>> GetAll()
        {
            return await _context.Tourists
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        // 5. Streamlined Update with Concurrency Handling
        public async Task<TouristPlace> Update(TouristPlace updatedPlace)
        {
            if (updatedPlace == null) throw new ArgumentNullException(nameof(updatedPlace));

            // Attach the entity and mark it as modified
            _context.Entry(updatedPlace).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check if the record actually exists in the DB
                if (!await TouristPlaceExists(updatedPlace.Id))
                {
                    return null;
                }
                else
                {
                    throw; // Re-throw if it's a different DB error
                }
            }

            return updatedPlace;
        }

        // Helper method for existence checks
        private async Task<bool> TouristPlaceExists(long id)
        {
            return await _context.Tourists.AnyAsync(e => e.Id == id);
        }
    }
}