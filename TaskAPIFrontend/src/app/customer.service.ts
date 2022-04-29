import { Injectable } from '@angular/core';
import { Customer } from './customer.model';
import {HttpClient} from '@angular/common/http'
@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  url:string="https://localhost:44309/api/Customers"
  customers:Customer[]=[];
  constructor(private http:HttpClient) {
   }


   getAllCustomers()
   {
      this.http.get(this.url).toPromise().then(
        res=>{this.customers=res as Customer[]}
      )
   }
}
