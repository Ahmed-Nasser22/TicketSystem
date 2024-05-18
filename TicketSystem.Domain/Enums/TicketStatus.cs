using System.Text.Json.Serialization;

namespace TicketSystem.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TicketStatus
    {
        New,
        Handled
    }
}
