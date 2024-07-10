import { makeApi, Zodios, type ZodiosOptions } from "@zodios/core";
import { z } from "zod";

const CreateCatalogItemRequest = z.object({
  version: z.string().min(1),
  isCommercial: z.boolean().optional(),
  annualCostPerSeat: z.number().optional(),
});
const CatalogItemResponse = z.object({
  vendor: z.string().min(1).max(100),
  application: z.string().min(1).max(100),
  version: z.string().min(1).max(10),
  annualCostPerSeat: z.number().optional(),
});

export const schemas = {
  CreateCatalogItemRequest,
  CatalogItemResponse,
};

const endpoints = makeApi([
  {
    method: "post",
    path: "/catalog/:vendor/:application",
    alias: "postCatalogVendorApplication",
    requestFormat: "json",
    parameters: [
      {
        name: "body",
        type: "Body",
        schema: CreateCatalogItemRequest,
      },
      {
        name: "vendor",
        type: "Path",
        schema: z.string(),
      },
      {
        name: "application",
        type: "Path",
        schema: z.string(),
      },
    ],
    response: CatalogItemResponse,
    errors: [
      {
        status: 400,
        description: `Bad Request`,
        schema: z.string(),
      },
    ],
  },
  {
    method: "get",
    path: "/catalog/:vendor/:application/:version",
    alias: "getCatalogVendorApplicationVersion",
    requestFormat: "json",
    parameters: [
      {
        name: "vendor",
        type: "Path",
        schema: z.string().regex(/^[a-zA-Z]+$/),
      },
      {
        name: "application",
        type: "Path",
        schema: z.string(),
      },
      {
        name: "version",
        type: "Path",
        schema: z.string(),
      },
    ],
    response: CatalogItemResponse,
    errors: [
      {
        status: 404,
        description: `Not Found`,
        schema: z.void(),
      },
    ],
  },
]);

export const api = new Zodios(endpoints);

export function createApiClient(baseUrl: string, options?: ZodiosOptions) {
  return new Zodios(baseUrl, endpoints, options);
}
