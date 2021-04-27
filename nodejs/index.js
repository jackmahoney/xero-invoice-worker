const express = require('express')
const app = express()
const port = 3000

function getPayload() {
    return {
        items: []
    }
}

app.get('/events', (req, res) => {
    res.json(getPayload())
})

app.listen(port, () => {
    console.log(`Events server listening at http://localhost:${port}`)
})