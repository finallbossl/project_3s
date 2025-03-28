using System.ComponentModel.DataAnnotations.Schema;
using FSA_3S.Models.Entities;
using Microsoft.EntityFrameworkCore;

[Table("mappingcontractclause")]
[PrimaryKey(nameof(ContractId), nameof(ClauseId))] // Khai báo khóa chính hỗn hợp
public class MappingContractClauseEntity
{
    public int ContractId { get; set; }
    public ContractEntity Contract { get; set; } = null!;

    public int ClauseId { get; set; }
    public ClauseEntity Clause { get; set; } = null!;
}