/**
 * A test server for event feed
 */
import express from "express";
import { getPayload } from "./lib";
const app = express();
const port = 3000;

app.get("/events", (req, res) => {
  const { pageSize, afterEventId } = req.query;
  const payload = getPayload(pageSize, afterEventId);
  console.info(`/events pageSize=${pageSize} lastId=${afterEventId}`);
  res.json(payload);
});

app.listen(port, () => {
  console.log(`Events server listening at http://localhost:${port}`);
});
