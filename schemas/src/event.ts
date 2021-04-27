/**
 * Type definitions for invoice events
 */
type Events = {
    items: InvoiceEvent[];
}

type InvoiceEvent = InvoiceCreatedEvent | InvoiceUpdatedEvent | InvoiceDeletedEvent;

enum EventType {
    INVOICE_CREATED = "INVOICE_CREATED", 
    INVOICE_UPDATED = "INVOICE_UPDATED", 
    INVOICE_DELETED = "INVOICE_DELETED"
}

enum InvoiceStatus {
    DRAFT = "DRAFT", 
    SENT = "SENT", 
    PAID = "PAID", 
    DELETED = "DELETED"
}

type LineItem = {
    lineItemId: string;
    description: string;
    quantity: number;
    unitCost: number;
    lineItemTotalCost: number;
};

type InvoiceCreatedOrUpdatedEventContent = {
    invoiceId: string;
    invoiceNumber: string;
    lineItems: LineItem[];
    status: InvoiceStatus;
    dueDateUtc: string;
    createdDateUtc: string;
    updatedDateUtc: string;
}

type InvoiceDeletedEventContent = {
    invoiceId: string;
}

type InvoiceCreatedEvent = {
    id: number;
    type: EventType.INVOICE_CREATED;
    createdDateUtc: string;
}
type InvoiceUpdatedEvent = {
    id: number;
    type: EventType.INVOICE_UPDATED;
    content: InvoiceCreatedOrUpdatedEventContent;
    createdDateUtc: string;
} 
type InvoiceDeletedEvent = {
    id: number;
    type: EventType.INVOICE_DELETED;
    content: InvoiceDeletedEventContent;
    createdDateUtc: string;
};
