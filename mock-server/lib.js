// override event type or response mode with environment variables
const maxId = 100;

export function getPayload(
  pageSize,
  afterEventId,
  mode = process.env.MODE === "increment" ? "increment" : "random",
  type = process.env.TYPE ?? "INVOICE_CREATED"
) {
  const after = afterEventId ? parseInt(afterEventId, 10) : 0;
  const page = pageSize ? parseInt(pageSize, 10) : 10;
  let items = [];
  for (let i = 0; i < page; i++) {
    // if increment mode make ids sequential else random
    const id = mode === "increment" ? after + i + 1 : randomId();
    if (mode === "increment" && id >= maxId) {
      break;
    }
    const invoiceId = `invoice-${id}`;
    const utc = new Date().toISOString();
    // content depends on event type
    const content =
      type === "INVOICE_DELETED"
        ? {
            invoiceId,
          }
        : {
            invoiceId,
            invoiceNumber: "123",
            lineItems: [
              {
                lineItemId: "123",
                description: "",
                quantity: 9.5,
                unitCost: 50,
                lineItemTotalCost: 475,
              },
            ],
            dueDateUtc: utc,
            createdDateUtc: utc,
            updatedDateUtc: utc,
          };
    items.push({
      id,
      type: type === "MIXED" ? randomType() : type,
      content,
      createdDateUtc: utc,
    });
  }
  return { items };
}

export function randomId() {
  const min = 1,
    max = 9999;
  return randomBetween(min, max);
}

export function randomBetween(min, max) {
  return Math.floor(Math.random() * (max - min + 1) + min);
}

export function randomType() {
  return ["INVOICE_DELETED", "INVOICE_CREATED", "INVOICE_UPDATED"][
    randomBetween(0, 2)
  ];
}
