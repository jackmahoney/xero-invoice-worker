using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace csharp.models
{
    [DataContract]
    public enum InvoiceStatus
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

    [DataContract]
    public enum EventType
    {
        [EnumMember(Value = "INVOICE_CREATED")]
        InvoiceCreated,
        [EnumMember(Value = "INVOICE_UPDATED")]
        InvoiceUpdated,
        [EnumMember(Value = "INVOICE_DELETED")]
        InvoiceDeleted
    }

    [DataContract]
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

    [DataContract]
    public class InvoiceCreatedOrUpdatedEventContent
    {
        [JsonPropertyName("invoiceId")]
        public string InvoiceId { get; set; }
        [JsonPropertyName("invoiceNumber")]
        public string InvoiceNumber { get; set; }
        [JsonPropertyName("lineItems")]
        public List<LineItem> LineItems { get; set; }
        [JsonPropertyName("status")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public InvoiceStatus Status { get; set; }
        [JsonPropertyName("dueDateUtc")]
        public DateTime DueDateUtc { get; set; }
        [JsonPropertyName("createdDateUtc")]
        public DateTime CreatedDateUtc { get; set; }
        [JsonPropertyName("updatedDateUtc")]
        public DateTime UpdatedDateUtc { get; set; }
    }

    [DataContract]
    public class InvoiceDeletedEventContent
    {
        [JsonPropertyName("invoiceId")]
        public string InvoiceId { get; set; }
    }
    [DataContract]
    public class Event
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("type")] 
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EventType Type { get; set; }
        [JsonPropertyName("createdDateUtc")]
        public DateTime CreatedDateUtc { get; set; }
        [JsonPropertyName("content")]
        public object Content;
    }

    [DataContract]
    public class EventsResponse
    {
        [JsonPropertyName("items")]
        public List<Event> Items { get; set; }
    }
}