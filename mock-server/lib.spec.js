import { getPayload } from "./lib";

describe("mock server", () => {
  it("can get payload", () => {
    const res = getPayload(10, 0, "increment", "INVOICE_CREATED");
    expect(res.items.length).toEqual(10);
    for (const item of res.items) {
        expect(item.type).toEqual("INVOICE_CREATED");
    }
  });
});
