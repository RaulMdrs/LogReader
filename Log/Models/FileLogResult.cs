namespace Log.Models;

public struct FileLogResult
{
    public string FilePath { get; set; }
    public int TotalLogs { get; set; }
    public int TotalErrors { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}