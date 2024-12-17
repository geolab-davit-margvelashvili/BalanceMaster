namespace BalanceMaster.DesktopApp;

public class ApiError
{
    public int StatusCode { get; set; }
    public string ErrorCode { get; set; }
    public string Message { get; set; }
    public string Detail { get; set; }
}