using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FSA_3S.Models; 
using FSA_3S.Models.Entities;

public class MappingUserAppointmentRepository(AppDbContext context) : IMappingUserAppointmentRepository
{
    private readonly AppDbContext _context = context;


    public async Task<IEnumerable<MappingUserAppointmentEntity>> GetAllAsync()
    {
        return await _context.MappingUserAppointments
        .Include(m => m.User) // Load thông tin User
        .Include(m => m.Appointment) // Load thông tin Appointment
        .ThenInclude(a => a.Customer) // Load luôn Customer nếu có
        .ToListAsync();
    }

    public async Task<MappingUserAppointmentEntity> GetByIdAsync(int id)
    {
        var mapping = await _context.MappingUserAppointments.FindAsync(id);
        return mapping;
    }

    public async Task<MappingUserAppointmentEntity> AddAsync(MappingUserAppointmentEntity mapping)
    {
        _context.MappingUserAppointments.Add(mapping);
        await _context.SaveChangesAsync();
        return mapping;
    }

    public async Task<MappingUserAppointmentEntity> UpdateAsync(MappingUserAppointmentEntity mapping)
    {
        _context.Entry(mapping).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return mapping;
    }

    public async Task DeleteAsync(int id)
    {
        var mapping = await GetByIdAsync(id);
        if (mapping != null)
        {
            _context.MappingUserAppointments.Remove(mapping);
            await _context.SaveChangesAsync();
        }
    }
}