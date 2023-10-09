using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ProgramManagement.Persistence.Entity;

public class ApiCallLog
{
    [JsonProperty(PropertyName = "id")]
    public string id { get; set; }
    public string? UserEmail { get; set; }
    public DateTime RequestTime { get; set; }
    public long ResponseMillis { get; set; }
    public int StatusCode { get; set; }
    [StringLength(10)]
    public string Method { get; set; }
    [StringLength(100)]
    public string Path { get; set; }
    [StringLength(150)]
    public string? QueryString { get; set; }
    [StringLength(1000)]
    public string? RequestBody { get; set; }
    [StringLength(50)]
    public string? RequestIp { get; set; }
    [StringLength(500)]
    public string? ResponseBody { get; set; }
    public DateTime ResponseTime { get; set; }
}