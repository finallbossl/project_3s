using FSA_3S.Enum;

public class UnableStaffResponse
{
    public bool Success { get; set; }
    public UserStatusEnum? Status { get; set; }

    public UnableStaffResponse() { }

    public UnableStaffResponse(bool success)
    {
        Success = success;
    }

    public UnableStaffResponse(bool success, UserStatusEnum? newStatus)
    {
        Success = success;
        Status = newStatus;
    }
}