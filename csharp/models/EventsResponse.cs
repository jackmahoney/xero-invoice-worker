using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace csharp.models
{
    internal enum InvoiceStatus
    {
        [EnumMember(Value = "DRAFT")]
        Draft,
        [EnumMember(Value = "SENT")]
        Sent,
        [EnumMember(Value = "PAID")]
        Paid,
        [EnumMember(Value = "DELETED")]
        Deleted
    }

    public enum EventType
    {
        [EnumMember(Value = "INVOICE_CREATED")]
        InvoiceCreated,
        [EnumMember(Value = "INVOICE_UPDATED")]
        InvoiceUpdated,
        [EnumMember(Value = "INVOICE_DELETED")]
        InvoiceDeleted
    }

    internal class LineItem
    {
        public string lineItemId { get; set; }
        public string description { get; set; }
        public decimal quantity { get; set; }
        public decimal unitCost { get; set; }
        public decimal lineItemTotalCost { get; set; }
    }

    internal class InvoiceCreatedOrUpdatedEventContent
    {
        [JsonPropertyName("invoiceId")]
        public string InvoiceId { get; set; }
        [JsonPropertyName("invoiceNumber")]
        public string InvoiceNumber { get; set; }
        [JsonPropertyName("lineItems")]
        public LineItem[] LineItems { get; set; }
        [JsonPropertyName("status")]
        public InvoiceStatus Status { get; set; }
        [JsonPropertyName("dueDateUtc")]
        public string DueDateUtc { get; set; }
        [JsonPropertyName("createdDateUtc")]
        public DateTime CreatedDateUtc { get; set; }
        [JsonPropertyName("updatedDateUtc")]
        public DateTime UpdatedDateUtc { get; set; }
    }

    internal class InvoiceDeletedEventContent
    {
        [JsonPropertyName("invoiceId")]
        public string InvoiceId { get; set; }
    }
    public class Event
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("type")] 
        public EventType Type { get; set; }
        [JsonPropertyName("createdDateUtc")]
        public DateTime CreatedDateUtc { get; set; }
        public object content;
    }

    public class EventsResponse
    {
        [JsonPropertyName("items")] public Event[] Items { get; set; }
    }
}