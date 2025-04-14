using FSA_3S.Models;
using FSA_3S.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ReportService
{
    private readonly AppDbContext _context;

    public ReportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReportEntity>> GetAllReportsAsync()
    {
        return await _context.Reports.ToListAsync();
    }

    public async Task<ReportEntity?> GetReportByIdAsync(int id)
    {
        return await _context.Reports.FirstOrDefaultAsync(r => r.ReportId == id);
    }
}
