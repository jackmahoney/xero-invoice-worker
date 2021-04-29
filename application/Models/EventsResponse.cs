using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Application.Models
{
    public enum InvoiceStatus
    {
        DRAFT,
        SENT,
        PAID,
        DELETED
    }

    public enum EventType
    {
        INVOICE_CREATED,
        INVOICE_UPDATED,
        INVOICE_DELETED
    }

    public class LineItem
    {
        [JsonPropertyName("lineItemId")]
        public string LineItemId { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        [JsonPropertyName("unitCost")]
        public decimal UnitCost { get; set; }
        [JsonPropertyName("lineItemTotalCost")]
        public decimal LineItemTotalCost { get; set; }
    }

    /**
     * API returns events that have different content depending on their type. This means the content has nullabe properties for those not shared for all events.
     * In production would ask endpoint owner if they can add a type filter param so we can deserialize event content into separate classes to avoid this nullable properties issue
     * Or use Newtonsoft instead of Json.Text deserializer
     */
#nullable enable
    [DataContract]
    public class EventContent
    {
        [JsonPropertyName("invoiceId")]
        public string InvoiceId { get; set; }
        [JsonPropertyName("invoiceNumber")]
        public string? InvoiceNumber { get; set; }
        [JsonPropertyName("lineItems")]
        public List<LineItem>? LineItems { get; set; }
        [JsonPropertyName("status")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public InvoiceStatus? Status { get; set; }
        [JsonPropertyName("dueDateUtc")]
        public DateTime? DueDateUtc { get; set; }
        [JsonPropertyName("createdDateUtc")]
        public DateTime? CreatedDateUtc { get; set; }
        [JsonPropertyName("updatedDateUtc")]
        public DateTime? UpdatedDateUtc { get; set; }
    }

    [DataContract]
    public class Event
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EventType Type { get; set; }
        [JsonPropertyName("createdDateUtc")]
        public DateTime CreatedDateUtc { get; set; }
        [JsonPropertyName("content")]
        public EventContent Content { get; set; }
    }

    [DataContract]
    public class EventsResponse
    {
        [JsonPropertyName("items")]
        public List<Event> Items { get; set; }
    }
}