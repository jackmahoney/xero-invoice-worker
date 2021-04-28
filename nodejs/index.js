const express = require('express')
const app = express()
const port = 3000

function getPayload() {
    return {
        items: [
            {
                id: 2,
                type: "INVOICE_CREATED",
                content: {
                    invoiceId: "123"
                },
                createdDateUtc: "2013-01-20T00:00:00Z"
            }
        ]
    }
}

app.get('/events', (req, res) => {
    console.info("/events 200")
    res.json(getPayload())
})

app.listen(port, () => {
    console.log(`Events server listening at http://localhost:${port}`)
})
