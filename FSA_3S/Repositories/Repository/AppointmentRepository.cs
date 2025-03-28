using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FSA_3S.Models;
using FSA_3S.Models.Entities;

public class AppointmentRepository(AppDbContext context) : IAppointmentRepository
{
    private readonly AppDbContext _context = context;

    public IQueryable<AppointmentEntity> Appointments => _context.Appointments;
    public async Task<IEnumerable<AppointmentEntity>> GetAllAsync()
    {
        return await _context.Appointments.ToListAsync();
    }

    public async Task<AppointmentEntity?> GetByIdAsync(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
        {
            return null; 
        }
        return appointment;
    }

    public async Task<AppointmentEntity> AddAsync(AppointmentEntity appointment)
    {
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task<AppointmentEntity> UpdateAsync(AppointmentEntity appointment)
    {
        _context.Entry(appointment).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task DeleteAsync(int id)
    {
        var appointment = await GetByIdAsync(id);
        if (appointment != null)
        {
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }
    }
}