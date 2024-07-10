import { test, expect} from '@playwright/test'
import {createApiClient, api, schemas } from '../client/client'
import { z} from 'zod';

test.describe("Adding Items to the Catalog", () => {

   
    test('Adding an Item', async ({request}) => {

       const client = createApiClient("http://localhost:1338");

       try {
       const response = await client.postCatalogVendorApplication({version: "1.19", isCommercial: true, annualCostPerSeat: 20});
       
     
       } catch (error) {

       }       

    

    });
} );