import { Injectable } from '@angular/core';
import { Msg } from './msg.model';
import {HttpClient} from '@angular/common/http'
@Injectable({
  providedIn: 'root'
})
export class MsgService {

  url:string="https://localhost:44309/api/Msgs/";
  Msgs:Msg[]=[];
  message:Msg=new Msg;

  constructor(private http:HttpClient) {

   }


   getAllMsgs()
   {
      this.http.get(this.url).toPromise().then(
        res=>{this.Msgs=res as Msg[]}
      )

   }

   postMsg(id:number)
   {
     if(this.message.MsgSubject==" "||this.message.MsgBody==" ")
     {
        alert("You must fill all data")
        return this.http.request("","")
     }
     else
     {
       this.message.status="Delivered";
      this.message.CustId=id;
      return this.http.post(this.url,this.message)
     }


   }
}
