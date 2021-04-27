using System;
using System.Text.Json.Serialization;

namespace csharp.events
{
    internal enum InvoiceStatus
    {
        DRAFT,
        SENT,
        PAID,
        DELETED
    }

    internal enum EventType
    {
        INVOICE_CREATED,
        INVOICE_UPDATED,
        INVOICE_DELETED
    }

    internal class LineItem
    {
        internal string lineItemId { get; set; }
        private string description { get; set; }
        private decimal quantity { get; set; }
        private decimal unitCost { get; set; }
        private decimal lineItemTotalCost { get; set; }
    }

    internal class InvoiceCreatedOrUpdatedEventContent
    {
        private string InvoiceId { get; set; }
        private string InvoiceNumber { get; set; }
        private LineItem[] lineItems { get; set; }
        private InvoiceStatus status { get; set; }
        private string dueDateUtc { get; set; }
        private DateTime createdDateUtc { get; set; }
        private DateTime updatedDateUtc { get; set; }
    }

    internal class InvoiceDeletedEventContent
    {
        private string invoiceId { get; set; }
    }

    internal class InvoiceCreatedEvent
    {
        private int id { get; set; }
        private EventType type { get; set; }
    }

    internal class InvoiceUpdatedEvent
    {
        private int id { get; set; }
        private EventType type { get; set; }
        private InvoiceCreatedOrUpdatedEventContent content { get; set; }
    }

    internal class InvoiceDeletedEvent
    {
        private int id { get; set; }
        private EventType type { get; set; }
        private InvoiceDeletedEventContent content { get; set; }
        private DateTime createdDateUtc { get; set; }
    }

    public class Event
    {
        [JsonPropertyName("invoiceId")] public string InvoiceId { get; set; }
    }

    public class Events
    {
        [JsonPropertyName("items")] public Event[] Items { get; set; }
    }
}