/**
 * A test server for event feed
 */

// override event type or response mode with environment variables

const mode = process.env.MODE === 'increment' ? 'increment' : 'random';
const type = process.env.TYPE ?? 'INVOICE_CREATED';

import express from 'express';
const app = express()
const port = 3000

export function getPayload(pageSize, afterEventId) {
    const after = afterEventId ?? 0;
    const page = pageSize ?? 10;
    let items = [];
    for(let i = 0; i < page; i++) {
        // if increment mode make ids sequential else random
        const id = mode === 'increment' ? (after + i) : randomId()
        const invoiceId = `invoice-${id}`
        const utc = new Date().toUTCString()
        // content depends on event type
        const content = type === 'INVOICE_DELETED' ? {
            invoiceId
        }: {
            invoiceId,
            invoiceNumber: "123",
            lineItems: [{
                lineItemId: "123",
                description: "",
                quantity: 9.5,
                unitCost: 50,
                lineItemTotalCost: 475
            }],
            dueDateUtc: utc,
            createdDateUtc: utc,
            updatedDateUtc: utc,
        };
        items.push({
            id,
            type,
            content,
            createdDateUtc: utc
        })
    }
    return { items };
}

export function randomId() {
    const min = 1, max = 9999;
    return Math.floor(Math.random()*(max-min+1)+min);
}

app.get('/events', (req, res) => {
    const { pageSize, afterEventId } = req.query;
    const payload = getPayload(pageSize, afterEventId)
    console.info(`/events pageSize=${pageSize} lastId=${afterEventId}`)
    res.json(payload)
})

app.listen(port, () => {
    console.log(`Events server listening at http://localhost:${port}`)
})
