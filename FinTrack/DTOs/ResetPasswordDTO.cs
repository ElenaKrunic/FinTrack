namespace FinTrack.DTOs;

public class ResetPasswordDTO
{
    public string Token { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}
